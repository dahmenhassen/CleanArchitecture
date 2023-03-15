using CleanArchitecture.Application.Common.Models;
using MediatR;

namespace CleanArchitecture.Application.Authentication.Commands.Login;

public record LoginCommandRequest : IRequest<ServiceResult<LoginCommandResponse>>
{
    public string UserName { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}