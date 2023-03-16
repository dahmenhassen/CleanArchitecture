using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using MediatR;

namespace CleanArchitecture.Application.User.Commands.UpdateUserRoles;

[Authorize(Roles = "Admin")]
public record UpdateUserRolesCommandRequest : IRequest<ServiceResult<UpdateUserRolesCommandResponse>>
{
    public required string Id { get; init; }
    public required IList<string> Roles { get; init; }
}