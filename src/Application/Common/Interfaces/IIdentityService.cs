using CleanArchitecture.Application.Common.Models;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

    Task<Result> ChangeRolesAsync(string userId, IEnumerable<string> roles);

    Task<bool> CheckPasswordAsync(string userId, string password);

    Task<IList<string>> GetUserRoleAsync(string userId);

    Task<Result> DeleteUserAsync(string userId);
}