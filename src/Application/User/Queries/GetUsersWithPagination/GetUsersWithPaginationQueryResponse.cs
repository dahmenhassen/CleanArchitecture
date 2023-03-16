using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.User.Queries.GetUsersWithPagination;

public class GetUsersWithPaginationQueryResponse : IMapFrom<UserInfo>
{
    public required string Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string FullName { get; set; }
    public required string UserName { get; set; }
    public required IList<string> Roles { get; set; }
}