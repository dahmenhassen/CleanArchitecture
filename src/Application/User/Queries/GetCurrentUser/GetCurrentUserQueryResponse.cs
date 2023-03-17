using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.User.Queries.GetCurrentUser;

public class GetCurrentUserQueryResponse : IMapFrom<UserInfo>
{
    public required string Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string FullName { get; set; }
    public required string UserName { get; set; }
    public required IList<string> Roles { get; set; }

    // public void Mapping(Profile profile)
    // {
    //     profile.CreateMap<UserInfo, GetCurrentUserQueryResponse>()
    //         .ForMember(
    //             dest => dest.Roles,
    //             opt => opt.Ignore()
    //         );
    // }
    // Todo: fix test warning
}