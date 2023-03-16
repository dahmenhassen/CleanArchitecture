using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.User.Commands.UpdateUserRoles;

public class UpdateUserRolesCommandHandler : IRequestHandler<UpdateUserRolesCommandRequest,
        ServiceResult<UpdateUserRolesCommandResponse>>
{
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUserService;

    public UpdateUserRolesCommandHandler(IIdentityService identityService, ICurrentUserService currentUserService)
    {
        _identityService = identityService;
        _currentUserService = currentUserService;
    }

    public async Task<ServiceResult<UpdateUserRolesCommandResponse>> Handle(UpdateUserRolesCommandRequest request,
        CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;

        if (string.IsNullOrEmpty(currentUserId))
        {
            throw new UnauthorizedAccessException();
        }

        if (request.Id == currentUserId && !request.Roles.Contains(Roles.Admin.ToString()))
        {
            throw new BadRequestException("you cant remove your admin role");
        }

        var changeRolesResult = await _identityService.ChangeRolesAsync(request.Id, request.Roles);

        if (!changeRolesResult.Succeeded)
        {
            throw new ValidationException(changeRolesResult.Errors);
        }

        return ServiceResult.Success(new UpdateUserRolesCommandResponse { Id = request.Id });
    }
}