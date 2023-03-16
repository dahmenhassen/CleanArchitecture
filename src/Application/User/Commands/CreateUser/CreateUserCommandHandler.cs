using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using MediatR;

namespace CleanArchitecture.Application.User.Commands.CreateUser;

public class
    CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, ServiceResult<CreateUserCommandResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;
    private readonly ITokenService _tokenService;

    public CreateUserCommandHandler(IApplicationDbContext context, ITokenService tokenService,
        IIdentityService identityService, ICurrentUserService currentUserService)
    {
        _context = context;
        _tokenService = tokenService;
        _identityService = identityService;
        _currentUserService = currentUserService;
    }

    public async Task<ServiceResult<CreateUserCommandResponse>> Handle(CreateUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        string? currentUserId = _currentUserService.UserId;
        
        List<string> roles = new() { Roles.User.ToString() };

        if (!string.IsNullOrEmpty(currentUserId))
        {
            IList<string> currentUserRoles = await _identityService.GetUserRolesAsync(currentUserId);
            if (currentUserRoles.Contains(Roles.Admin.ToString()))
            {
                roles = request.Roles;
            }
        }

        (Result createUserResult, string userId) =
            await _identityService.CreateUserAsync(request.UserName, request.Password);

        if (!createUserResult.Succeeded)
        {
            throw new ValidationException(createUserResult.Errors);
        }

        Result changeRoleResult = await _identityService.ChangeRolesAsync(userId, roles);

        if (!changeRoleResult.Succeeded)
        {
            throw new ValidationException(changeRoleResult.Errors);
        }

        UserInfo userInfo = new()
        {
            Id = userId, UserName = request.UserName, FirstName = request.FirstName, LastName = request.LastName
        };

        _context.UserInfos.Add(userInfo);

        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(new CreateUserCommandResponse { Id = userId });
    }
}