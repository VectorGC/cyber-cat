using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AuthService.Application;
using AuthService.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure.Authorization;
using Shared.Server.Exceptions;

namespace AuthService.Infrastructure;

internal class UserManagerRepository : IUserRepository
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly RoleManager<RoleEntity> _roleManager;
    private readonly ILogger<UserManagerRepository> _logger;

    public UserManagerRepository(UserManager<UserEntity> userManager, RoleManager<RoleEntity> roleManager, ILogger<UserManagerRepository> logger)
    {
        _logger = logger;
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
            UserName = $"{name}#{nextId}", // Identity requires that UserName be unique for everyone. ¯\_(ツ)_/¯
            FirstName = name,
        };

        var result = await _userManager.CreateAsync(userModel, password);
        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            switch (error.Code)
            {
                case nameof(IdentityErrorDescriber.DuplicateEmail):
                    return new CreateUserResult(false, UserRepositoryError.DuplicateEmail, userModel);
                case nameof(IdentityErrorDescriber.InvalidUserName):
                    return new CreateUserResult(false, UserRepositoryError.InvalidUserNameCharacters, userModel);
                default:
                    _logger.LogError("Identity error '{Code}' {Description}", error.Code, error.Description);
                    return new CreateUserResult(false, UserRepositoryError.Unknown, userModel);
            }
        }

        return new CreateUserResult(true, UserRepositoryError.None, userModel);
    }

    public async Task<DeleteUserResult> DeleteUser(UserId userId)
    {
        var user = await _userManager.FindByIdAsync(userId.Value.ToString());

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            var errors = string.Join(';', result.Errors.Select(e => e.Description));
            return new DeleteUserResult(false, errors, user);
        }

        return new DeleteUserResult(true, string.Empty, user);
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

    public async Task<UpdateUserResult> UpdateUser(UserEntity userModel)
    {
        var result = await _userManager.UpdateAsync(userModel);
        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            switch (error.Code)
            {
                default:
                    return new UpdateUserResult(false, UserRepositoryError.Unknown);
            }
        }

        return new UpdateUserResult(true, UserRepositoryError.None);
    }

    public async Task<int> GetUsersCountWithRole(string roleId)
    {
        var roleModel = await _roleManager.FindByNameAsync(roleId);
        if (roleModel == null)
            throw new ServiceException($"Роль '{roleId}' не найдена", HttpStatusCode.NotFound);

        var users = await _userManager.GetUsersInRoleAsync(roleModel.Name);

        return users.Count;
    }

    public async Task<string> GetRoleId(string roleId)
    {
        var roleModel = await _roleManager.FindByIdAsync(roleId);
        if (roleModel == null)
            throw new ServiceException($"Роль '{roleId}' не найдена", HttpStatusCode.NotFound);

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