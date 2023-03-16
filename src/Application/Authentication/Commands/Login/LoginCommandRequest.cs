using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Application.Common.Models;
using MediatR;

namespace CleanArchitecture.Application.Authentication.Commands.Login;

public record LoginCommandRequest : IRequest<ServiceResult<LoginCommandResponse>>
{
    [DataType(DataType.EmailAddress)]
    public required string UserName { get; init; }
    public required string Password { get; init; }
}