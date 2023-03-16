using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using MediatR;

namespace CleanArchitecture.Application.User.Commands.UpdateUser;

[Authorize(Roles = "Admin,User")]
public record UpdateUserCommandRequest : IRequest<ServiceResult<UpdateUserCommandResponse>>
{
    public required string Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? Password { get; init; }
    public string? ConfirmPassword { get; init; }
}