using System.Web;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.User.Commands.ResetUserPassword;

public class ResetUserPasswordCommandHandler : IRequestHandler<ResetUserPasswordCommandRequest,
    ServiceResult<ResetUserPasswordCommandResponse>>
{
    private readonly IIdentityService _identityService;

    public ResetUserPasswordCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<ServiceResult<ResetUserPasswordCommandResponse>> Handle(ResetUserPasswordCommandRequest request,
        CancellationToken cancellationToken)
    {
        var resetResult =
            await _identityService.ResetPasswordAsync(request.UserName, request.Token, request.NewPassword);

        if (!resetResult.Succeeded)
        {
            throw new ValidationException(resetResult.Errors);
        }

        return ServiceResult.Success(new ResetUserPasswordCommandResponse { UserName = request.UserName });
    }
}