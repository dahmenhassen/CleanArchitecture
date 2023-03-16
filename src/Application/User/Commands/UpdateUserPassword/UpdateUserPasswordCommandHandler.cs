using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.User.Commands.UpdateUserPassword;

public class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommandRequest,
    ServiceResult<UpdateUserPasswordCommandResponse>>
{
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUserService;

    public UpdateUserPasswordCommandHandler(IIdentityService identityService, ICurrentUserService currentUserService)
    {
        _identityService = identityService;
        _currentUserService = currentUserService;
    }

    public async Task<ServiceResult<UpdateUserPasswordCommandResponse>> Handle(UpdateUserPasswordCommandRequest request,
        CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;

        if (string.IsNullOrEmpty(currentUserId))
        {
            throw new UnauthorizedAccessException();
        }

        var changePasswordResult =
            await _identityService.ChangePasswordAsync(currentUserId, request.CurrentPassword, request.NewPassword);

        if (!changePasswordResult.Succeeded)
        {
            throw new ValidationException(changePasswordResult.Errors);
        }

        return ServiceResult.Success(new UpdateUserPasswordCommandResponse { Id = currentUserId });
    }
}