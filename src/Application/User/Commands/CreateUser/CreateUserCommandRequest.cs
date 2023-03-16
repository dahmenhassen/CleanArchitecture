using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Application.Common.Models;
using MediatR;

namespace CleanArchitecture.Application.User.Commands.CreateUser;

public record CreateUserCommandRequest : IRequest<ServiceResult<CreateUserCommandResponse>>
{
    [DataType(DataType.EmailAddress)]
    public required string UserName { get; init; }

    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Password { get; init; }
    public required string ConfirmPassword { get; init; }
    public required List<string> Roles { get; init; }
}