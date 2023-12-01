using System.Linq;
using System.Threading.Tasks;
using AuthService.Application;
using AuthService.Domain;
using Microsoft.AspNetCore.Identity;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure.Authorization;
using Shared.Server.Infrastructure.Exceptions;

namespace AuthService.Infrastructure;

internal class UserManagerRepository : IUserRepository
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly RoleManager<RoleEntity> _roleManager;

    public UserManagerRepository(UserManager<UserEntity> userManager, RoleManager<RoleEntity> roleManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<CreateUserResult> CreateUser(string email, string password, string name)
    {
        var nextId = _userManager.Users.Count() + 1;
        var userModel = new UserEntity
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

    public async Task<UserEntity> FindByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user;
    }

    public async Task<bool> CheckPasswordAsync(UserEntity user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task SetAuthenticationTokenAsync(UserEntity user, AuthorizationToken token)
    {
        await _userManager.SetAuthenticationTokenAsync(user, token.Type, token.TokenName, token.Value);
    }

    public async Task<UserEntity> GetUser(UserId userId)
    {
        var userModel = await _userManager.FindByIdAsync(userId.Value.ToString());
        return userModel;
    }

    public async Task<SaveUserResult> SaveUser(UserEntity userModel)
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

    public async Task<int> GetUsersCountWithRole(string roleId)
    {
        var roleModel = await _roleManager.FindByNameAsync(roleId);
        if (roleModel == null)
            throw new RoleNotFoundException(roleId);

        var users = await _userManager.GetUsersInRoleAsync(roleModel.Name);

        return users.Count;
    }

    public async Task<string> GetRoleId(string roleId)
    {
        var roleModel = await _roleManager.FindByIdAsync(roleId);
        if (roleModel == null)
            throw new RoleNotFoundException(roleId);

        return roleModel.Id;
    }

    public async Task<bool> RoleExists(string roleId)
    {
        return await _roleManager.FindByIdAsync(roleId) != null;
    }

    public async Task<CreateRoleResult> CreateRole(string roleId)
    {
        var roleModel = new RoleEntity(roleId);
        var result = await _roleManager.CreateAsync(roleModel);
        if (!result.Succeeded)
        {
            var errors = string.Join(';', result.Errors.Select(e => e.Description));
            return new CreateRoleResult(false, errors);
        }

        return new CreateRoleResult(true, string.Empty);
    }
}