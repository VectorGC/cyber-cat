using System.Linq;
using System.Threading.Tasks;
using AuthService.Repositories.InternalModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Shared.Server.Data;
using Shared.Server.Exceptions.AuthService;
using Shared.Server.Ids;

namespace AuthService.Repositories;

internal class AuthUserManagerRepository : IAuthUserRepository
{
    public int Count => _userManager.Users.Count();

    private readonly UserManager<UserDbModel> _userManager;

    public AuthUserManagerRepository(UserManager<UserDbModel> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UserDto> Create(string email, string password, string name)
    {
        var authUser = new UserDbModel(name, email, this);

        var result = await _userManager.CreateAsync(authUser, password);
        if (!result.Succeeded)
        {
            var errors = string.Join(';', result.Errors.Select(e => e.Description));
            throw new IdentityUserException(errors);
        }

        return authUser.ToDto();
    }

    public async Task Remove(UserId userId)
    {
        var user = await _userManager.FindByIdAsync(userId.Value.ToString());

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            var errors = string.Join(';', result.Errors.Select(e => e.Description));
            throw new IdentityUserException(errors);
        }
    }

    public async Task<UserDto> FindByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user?.ToDto();
    }

    public async Task<bool> CheckPasswordAsync(UserId userId, string password)
    {
        var user = await _userManager.FindByIdAsync(userId.Value.ToString());
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task SetJwtAuthenticationAccessTokenAsync(UserId userId, string accessToken)
    {
        var user = await _userManager.FindByIdAsync(userId.Value.ToString());
        await _userManager.SetAuthenticationTokenAsync(user, JwtBearerDefaults.AuthenticationScheme, "access_token", accessToken);
    }
}