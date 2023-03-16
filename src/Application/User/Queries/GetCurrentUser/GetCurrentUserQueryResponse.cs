using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.User.Queries.GetCurrentUser;

public class GetCurrentUserQueryResponse : IMapFrom<UserInfo>
{
    public string Id { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public IList<string> Roles { get; set; } = new List<string>();
}