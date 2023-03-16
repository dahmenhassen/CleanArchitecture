using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using MediatR;

namespace CleanArchitecture.Application.User.Queries.GetCurrentUser;

[Authorize(Roles = "Admin,User")]
public record GetCurrentUserQueryRequest : IRequest<ServiceResult<GetCurrentUserQueryResponse>>;