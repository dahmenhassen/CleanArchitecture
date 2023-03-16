using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.User.Commands.UpdateUser;

public class
    UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, ServiceResult<UpdateUserCommandResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUserService;

    public UpdateUserCommandHandler(IApplicationDbContext context, IIdentityService identityService,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _identityService = identityService;
        _currentUserService = currentUserService;
    }

    public async Task<ServiceResult<UpdateUserCommandResponse>> Handle(UpdateUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;

        if (string.IsNullOrEmpty(currentUserId))
        {
            throw new UnauthorizedAccessException();
        }

        var currentUserRoles = await _identityService.GetUserRolesAsync(currentUserId);

        if (!currentUserRoles.Contains(Roles.Admin.ToString()) && request.Id != currentUserId)
        {
            throw new ForbiddenAccessException();
        }

        var userInfo = await _context.UserInfos.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

        if (userInfo is null)
        {
            throw new NotFoundException(ServiceError.UserNotFound.Message);
        }

        userInfo.FirstName = request.FirstName;
        userInfo.LastName = request.LastName;

        await _context.SaveChangesAsync(cancellationToken);

        if (!string.IsNullOrEmpty(request.Password))
        {
            var changePasswordResult = await _identityService.ChangePasswordAsync(request.Id, request.Password);

            if (!changePasswordResult.Succeeded)
            {
                throw new ValidationException(changePasswordResult.Errors);
            }
        }

        return ServiceResult.Success(new UpdateUserCommandResponse { Id = request.Id });
    }
}