using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using MediatR;

namespace CleanArchitecture.Application.User.Commands.DeleteUser;

[Authorize(Roles = "Admin")]
public record DeleteUserCommandRequest(string Id) : IRequest<ServiceResult<DeleteUserCommandResponse>>;