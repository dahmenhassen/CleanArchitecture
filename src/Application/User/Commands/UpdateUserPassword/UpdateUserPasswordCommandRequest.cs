using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using MediatR;

namespace CleanArchitecture.Application.User.Commands.UpdateUserPassword;

[Authorize(Roles = "Admin,User")]
public record UpdateUserPasswordCommandRequest : IRequest<ServiceResult<UpdateUserPasswordCommandResponse>>
{
    public required string CurrentPassword { get; init; }
    public required string NewPassword { get; init; }
    public required string ConfirmNewPassword { get; init; }
}