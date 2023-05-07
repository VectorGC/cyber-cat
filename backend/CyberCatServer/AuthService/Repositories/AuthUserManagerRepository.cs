using AuthService.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Shared.Exceptions;

namespace AuthService.Repositories;

public class AuthUserManagerRepository : IAuthUserRepository
{
    private readonly UserManager<User> _userManager;

    public AuthUserManagerRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task Add(IUser user, string password)
    {
        var authUser = user as User ?? new User(user);

        var result = await _userManager.CreateAsync(authUser, password);
        if (!result.Succeeded)
        {
            var errors = string.Join(';', result.Errors);
            throw new IdentityUserException(errors);
        }
    }

    public async Task Remove(IUser user)
    {
        var authUser = user as User ?? new User(user);

        var result = await _userManager.DeleteAsync(authUser);
        if (!result.Succeeded)
        {
            var errors = string.Join(';', result.Errors);
            throw new IdentityUserException(errors);
        }
    }

    public async Task<IUser?> FindByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<bool> CheckPasswordAsync(IUser user, string password)
    {
        var authUser = user as User ?? new User(user);
        return await _userManager.CheckPasswordAsync(authUser, password);
    }

    public async Task SetJwtAuthenticationAccessTokenAsync(IUser user, string? accessToken)
    {
        var authUser = user as User ?? new User(user);
        await _userManager.SetAuthenticationTokenAsync(authUser, JwtBearerDefaults.AuthenticationScheme, "access_token", accessToken);
    }
}