using System.Linq;
using System.Threading.Tasks;
using AuthService.Domain;
using AuthService.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure.Authorization;
using Shared.Server.Infrastructure.Exceptions;

namespace AuthService.Infrastructure;

internal class UserManagerRepository : IUserRepository
{
    private readonly UserManager<UserModel> _userManager;
    private readonly RoleManager<RoleModel> _roleManager;

    public UserManagerRepository(UserManager<UserModel> userManager, RoleManager<RoleModel> roleManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<CreateUserResult> CreateUser(string email, string password, string name)
    {
        var nextId = _userManager.Users.Count() + 1;
        var userModel = new UserModel
        {
            Id = nextId.ToString(),
            Email = email,
            UserName = name,
        };

        var result = await _userManager.CreateAsync(userModel, password);
        if (!result.Succeeded)
        {
            var errors = string.Join(';', result.Errors.Select(e => e.Description));
            return new CreateUserResult(false, errors, userModel);
        }

        return new CreateUserResult(true, string.Empty, userModel);
    }

    public async Task<RemoveUserResult> RemoveUser(UserId userId)
    {
        var user = await _userManager.FindByIdAsync(userId.Value.ToString());

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            var errors = string.Join(';', result.Errors.Select(e => e.Description));
            return new RemoveUserResult(false, errors, user);
        }

        return new RemoveUserResult(true, string.Empty, user);
    }

    public async Task<UserModel> FindByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user;
    }

    public async Task<bool> CheckPasswordAsync(UserModel user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task SetAuthenticationTokenAsync(UserModel user, AuthorizationToken token)
    {
        await _userManager.SetAuthenticationTokenAsync(user, token.Type, token.TokenName, token.Value);
    }

    public async Task<UserModel> GetUser(UserId userId)
    {
        var userModel = await _userManager.FindByIdAsync(userId.Value.ToString());
        return userModel;
    }

    public async Task<SaveUserResult> SaveUser(UserModel userModel)
    {
        if (userModel == null)
        {
            return new SaveUserResult(false, "User can't be null");
        }

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

    public async Task<CreateRoleResult> CreateRole(Role role)
    {
        var roleModel = new RoleModel(role)
        {
            Id = role.Id
        };

        var result = await _roleManager.CreateAsync(roleModel);
        if (!result.Succeeded)
        {
            var errors = string.Join(';', result.Errors.Select(e => e.Description));
            return new CreateRoleResult(false, errors);
        }

        return new CreateRoleResult(true, string.Empty);
    }
}