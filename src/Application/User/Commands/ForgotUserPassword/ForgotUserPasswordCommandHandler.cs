using System.Web;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.User.Commands.ForgotUserPassword;

public class ForgotUserPasswordCommandHandler : IRequestHandler<ForgotUserPasswordCommandRequest,
    ServiceResult<ForgotUserPasswordCommandResponse>>
{
    private readonly IIdentityService _identityService;

    public ForgotUserPasswordCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<ServiceResult<ForgotUserPasswordCommandResponse>> Handle(ForgotUserPasswordCommandRequest request,
        CancellationToken cancellationToken)
    {
        var token = await _identityService.GeneratePasswordResetTokenAsync(request.UserName);

        var encodedToken = HttpUtility.UrlEncode(request.UrlCallback + token);

        // Todo: send email service

        Console.WriteLine($"Url of reset token is: {encodedToken}");

        return ServiceResult.Success(new ForgotUserPasswordCommandResponse { UserName = request.UserName });
    }
}