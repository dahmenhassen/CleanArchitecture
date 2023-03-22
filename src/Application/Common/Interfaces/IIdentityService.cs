using CleanArchitecture.Application.Common.Models;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);
    Task<string?> GetUserIdAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

    Task<Result> ChangeRolesAsync(string userId, IEnumerable<string> roles);

    Task<bool> CheckPasswordAsync(string userId, string password);
    
    Task<Result> ChangePasswordAsync(string userId, string password);
    
    Task<string> GeneratePasswordResetTokenAsync(string userName);
    
    Task<Result> ResetPasswordAsync(string userName, string token, string password);
    
    Task<Result> ChangePasswordAsync(string userId, string currentPassword, string newPassword);

    Task<IList<string>> GetUserRolesAsync(string userId);

    Task<Result> DeleteUserAsync(string userId);
}