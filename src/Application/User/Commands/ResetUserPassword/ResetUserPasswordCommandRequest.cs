using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using MediatR;

namespace CleanArchitecture.Application.User.Commands.ResetUserPassword;

public record ResetUserPasswordCommandRequest : IRequest<ServiceResult<ResetUserPasswordCommandResponse>>
{
    [DataType(DataType.EmailAddress)]
    public required string UserName { get; init; }

    public required string Token { get; init; }

    public required string NewPassword { get; init; }

    public required string ConfirmPassword { get; init; }
}