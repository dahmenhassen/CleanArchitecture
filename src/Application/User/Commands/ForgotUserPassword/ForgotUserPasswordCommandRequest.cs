using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using MediatR;

namespace CleanArchitecture.Application.User.Commands.ForgotUserPassword;

public record ForgotUserPasswordCommandRequest : IRequest<ServiceResult<ForgotUserPasswordCommandResponse>>
{
    [DataType(DataType.EmailAddress)]
    public required string UserName { get; init; }

    public required string UrlCallback { get; init; }
}