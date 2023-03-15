using CleanArchitecture.Application.Common.Models;
using MediatR;

namespace CleanArchitecture.Application.User.Queries.GetCurrent;

public record GetCurrentUserQueryRequest : IRequest<ServiceResult<GetCurrentUserQueryResponse>>;