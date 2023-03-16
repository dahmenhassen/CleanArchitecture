﻿using System.Security.Claims;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        ApplicationUser user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
    {
        ApplicationUser user = new() { UserName = userName, Email = userName };

        IdentityResult result = await _userManager.CreateAsync(user, password);

        return (result.ToApplicationResult(), user.Id);
    }

    public async Task<Result> ChangeRolesAsync(string userId, IEnumerable<string> roles)
    {
        ApplicationUser user = await GetUserAsync(userId);
        IList<string> userRoles = await GetUserRoleAsync(userId);
        IdentityResult removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
        if (!removeResult.Succeeded)
        {
            return removeResult.ToApplicationResult();
        }

        IdentityResult addResult = await _userManager.AddToRolesAsync(user, roles);

        return addResult.ToApplicationResult();
    }

    public async Task<bool> CheckPasswordAsync(string userId, string password)
    {
        ApplicationUser user = await GetUserAsync(userId);
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        ApplicationUser? user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        ApplicationUser? user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return false;
        }

        ClaimsPrincipal principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        AuthorizationResult result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        ApplicationUser user = await GetUserAsync(userId);

        return await DeleteUserAsync(user);
    }

    public async Task<IList<string>> GetUserRoleAsync(string userId)
    {
        ApplicationUser user = await GetUserAsync(userId);
        return await _userManager.GetRolesAsync(user);
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        IdentityResult result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

    private async Task<ApplicationUser> GetUserAsync(string userId)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            throw new NotFoundException(ServiceError.UserNotFound.Message);
        }

        return user;
    }
}