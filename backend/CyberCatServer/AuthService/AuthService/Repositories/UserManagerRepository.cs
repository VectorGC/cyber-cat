using System.Linq;
using System.Threading.Tasks;
using AuthService.Repositories.InternalModels;
using Microsoft.AspNetCore.Identity;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure.Authorization;
using Shared.Server.Infrastructure.Exceptions;

namespace AuthService.Repositories;

internal class UserManagerRepository : IUserRepository
{
    private readonly UserManager<UserDbModel> _userManager;
    private readonly RoleManager<RoleDbModel> _roleManager;

    public UserManagerRepository(UserManager<UserDbModel> userManager, RoleManager<RoleDbModel> roleManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<User> CreateUser(string email, string password, string name)
    {
        var nextId = _userManager.Users.Count() + 1;
        var authUser = new UserDbModel
        {
            Id = nextId.ToString(),
            Email = email,
            UserName = name,
        };

        var result = await _userManager.CreateAsync(authUser, password);
        if (!result.Succeeded)
        {
            var errors = string.Join(';', result.Errors.Select(e => e.Description));
            throw new IdentityException(errors);
        }

        return authUser.ToDomainModel();
    }

    public async Task Remove(UserId userId)
    {
        var user = await _userManager.FindByIdAsync(userId.Value.ToString());

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            var errors = string.Join(';', result.Errors.Select(e => e.Description));
            throw new IdentityException(errors);
        }
    }

    public async Task<User> FindByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user?.ToDomainModel();
    }

    public async Task<bool> CheckPasswordAsync(UserId userId, string password)
    {
        var user = await _userManager.FindByIdAsync(userId.Value.ToString());
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task SetAuthenticationTokenAsync(UserId id, AuthorizationToken token)
    {
        var user = await _userManager.FindByIdAsync(id.Value.ToString());
        await _userManager.SetAuthenticationTokenAsync(user, token.Type, token.TokenName, token.Value);
    }

    public async Task<User> GetUser(UserId userId)
    {
        var userModel = await _userManager.FindByIdAsync(userId.Value.ToString());
        return userModel.ToDomainModel();
    }

    public async Task<SaveUserResult> SaveUser(User user)
    {
        var userModel = await _userManager.FindByIdAsync(user.Id.Value.ToString());
        if (userModel == null)
        {
            return new SaveUserResult(false, $"User with id '{user.Id}' not found");
        }

        var roleId = await GetRoleId(user.Role);
        userModel.SetData(user, roleId);

        var result = await _userManager.UpdateAsync(userModel);
        if (!result.Succeeded)
        {
            var errors = string.Join(';', result.Errors.Select(e => e.Description));
            return new SaveUserResult(false, errors);
        }

        return new SaveUserResult(true, string.Empty);
    }

    public async Task<int> GetUsersCountWithRole(Role role)
    {
        var roleModel = await _roleManager.FindByNameAsync(role.Id);
        if (roleModel == null)
            throw new RoleNotFoundException(role);

        var users = await _userManager.GetUsersInRoleAsync(roleModel.Name);

        return users.Count;
    }

    public async Task<string> GetRoleId(Role role)
    {
        var roleModel = await _roleManager.FindByIdAsync(role.Id);
        if (roleModel == null)
            throw new RoleNotFoundException(role);

        return roleModel.Id;
    }

    public async Task<bool> RoleExists(Role role)
    {
        return await _roleManager.FindByIdAsync(role.Id) != null;
    }

    public async Task<AddRoleResult> CreateRole(Role role)
    {
        var roleModel = new RoleDbModel(role)
        {
            Id = role.Id
        };

        var result = await _roleManager.CreateAsync(roleModel);
        if (!result.Succeeded)
        {
            var errors = string.Join(';', result.Errors.Select(e => e.Description));
            return new AddRoleResult(false, errors);
        }

        return new AddRoleResult(true, string.Empty);
    }
}