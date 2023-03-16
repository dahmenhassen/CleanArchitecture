using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using MediatR;

namespace CleanArchitecture.Application.User.Queries.GetUsersWithPagination;

[Authorize(Roles = "Admin")]
public record GetUsersWithPaginationQueryRequest : IRequest<ServiceResult<PaginatedList<GetUsersWithPaginationQueryResponse>>>
{
    public string? UserNameLike { get; init; }
    public required string Sort { get; init; }
    public required int PageNumber { get; init; }
    public required int PageSize { get; init; }
}