using AuthService.Repositories.InternalModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Shared.Models;
using Shared.Models.Models;
using Shared.Server.Exceptions;

namespace AuthService.Repositories;

internal class AuthUserManagerRepository : IAuthUserRepository
{
    private readonly UserManager<User> _userManager;

    public AuthUserManagerRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task Add(IUser user, string password)
    {
        var authUser = new User(user);

        var result = await _userManager.CreateAsync(authUser, password);
        if (!result.Succeeded)
        {
            var errors = string.Join(';', result.Errors.Select(e => e.Description));
            throw new IdentityUserException(errors);
        }
    }

    public async Task Remove(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            var errors = string.Join(';', result.Errors.Select(e => e.Description));
            throw new IdentityUserException(errors);
        }
    }

    public async Task<IUser> FindByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<bool> CheckPasswordAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task SetJwtAuthenticationAccessTokenAsync(string email, string accessToken)
    {
        var user = await _userManager.FindByEmailAsync(email);
        await _userManager.SetAuthenticationTokenAsync(user, JwtBearerDefaults.AuthenticationScheme, "access_token", accessToken);
    }
}