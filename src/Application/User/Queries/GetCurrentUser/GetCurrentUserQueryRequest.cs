using CleanArchitecture.Application.Common.Models;
using MediatR;

namespace CleanArchitecture.Application.User.Queries.GetCurrentUser;

public record GetCurrentUserQueryRequest : IRequest<ServiceResult<GetCurrentUserQueryResponse>>;