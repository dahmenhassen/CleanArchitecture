using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Application.Common.Models;
using MediatR;

namespace CleanArchitecture.Application.User.Commands.CreateUser;

public record CreateUserCommandRequest : IRequest<ServiceResult<CreateUserCommandResponse>>
{
    [DataType(DataType.EmailAddress)] public string UserName { get; init; } = string.Empty;

    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string ConfirmPassword { get; init; } = string.Empty;
}