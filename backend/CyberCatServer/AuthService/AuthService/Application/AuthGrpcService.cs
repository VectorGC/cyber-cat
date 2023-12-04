using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AuthService.Infrastructure;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure.Authorization;
using Shared.Server.Application.Services;
using Shared.Server.Infrastructure.Exceptions;

namespace AuthService.Application;

public class AuthGrpcService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly UserEntityMapper _userEntityMapper;

    public AuthGrpcService(IUserRepository userRepository, ITokenService tokenService, UserEntityMapper userEntityMapper)
    {
        _userEntityMapper = userEntityMapper;
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<UserId> CreateUser(CreateUserArgs args)
    {
        var (email, password, userName, roles) = args;
        var result = await _userRepository.CreateUser(email, password, userName);
        if (!result.Success)
        {
            switch (result.Error)
            {
                case UserRepositoryError.DuplicateEmail:
                    throw new ServiceException($"Email '{email}' уже зарегистрирован", HttpStatusCode.Ambiguous);
                case UserRepositoryError.InvalidUserNameCharacters:
                    throw new ServiceException($"Имя пользователя содержит недопустимые символы", HttpStatusCode.Ambiguous);
                default:
                    throw new ServiceException("Неизвестная ошибка при регистрации. Обратитесь к администратору", HttpStatusCode.Conflict);
            }
        }

        if (roles != null)
        {
            result.CreatedUser.Roles = roles.Values;
            var saveResult = await _userRepository.UpdateUser(result.CreatedUser);
            if (!saveResult.Success)
            {
                throw new ServiceException("Неизвестная ошибка при регистрации. Обратитесь к администратору", HttpStatusCode.UnprocessableEntity);
            }
        }


        return new UserId(result.CreatedUser.Id);
    }

    public async Task RemoveUser(RemoveUserArgs args)
    {
        var (userId, password) = args;
        var user = await _userRepository.GetUser(userId);
        if (user == null)
        {
            throw new ServiceException("Пользователь не найден", HttpStatusCode.NotFound);
        }

        var isPasswordValid = await _userRepository.CheckPasswordAsync(user, password);
        if (!isPasswordValid)
        {
            throw new ServiceException("Неверный пароль", HttpStatusCode.Forbidden);
        }

        var result = await _userRepository.DeleteUser(userId);
        if (!result.Success)
        {
            throw new ServiceException("Неизвестная ошибка при удалении. Обратитесь к администратору", HttpStatusCode.UnprocessableEntity);
        }
    }

    public async Task<UserId> FindByEmail(FindByEmailArgs args)
    {
        var user = await _userRepository.FindByEmailAsync(args.Email);
        if (user == null)
            return null;
        return new UserId(user.Id);
    }

    public async Task<AuthorizationToken> GetAccessToken(GetAccessTokenArgs args)
    {
        var email = args.Email;
        var password = args.Password;

        var user = await _userRepository.FindByEmailAsync(email);
        if (user == null)
        {
            throw new ServiceException("Пользователь не найден", HttpStatusCode.NotFound);
        }

        var isPasswordValid = await _userRepository.CheckPasswordAsync(user, password);
        if (!isPasswordValid)
        {
            throw new ServiceException("Неверный пароль", HttpStatusCode.Forbidden);
        }

        var token = _tokenService.CreateToken(user);
        await _userRepository.SetAuthenticationTokenAsync(user, token);

        return token;
    }

    public async Task<List<UserModel>> GetUsersByIds(List<UserId> userIds)
    {
        var users = new List<UserModel>();
        foreach (var id in userIds)
        {
            var userEntity = await _userRepository.GetUser(id);
            users.Add(_userEntityMapper.ToDomain(userEntity));
        }

        return users;
    }

    public async Task<AuthorizationToken> GetAccessTokenWithVk(GetAccessTokenWithVkArgs args)
    {
        var (email, firstName, vkId, roles) = args;

        var user = await _userRepository.FindByEmailAsync(email);
        if (user == null)
        {
            var userId = await CreateUser(new CreateUserArgs(email, vkId, firstName, roles));
            user = await _userRepository.GetUser(userId);
        }

        var accessToken = await GetAccessToken(new GetAccessTokenArgs(email, vkId));
        if (accessToken is JwtAccessToken jwtAccessToken)
        {
            return new VkAccessToken()
            {
                JwtAccessToken = jwtAccessToken
            };
        }

        throw new ServiceException("Неизвестный тип токена", HttpStatusCode.Forbidden);
    }

    public async Task RemoveUserWithVk(RemoveUserWithVkArgs args)
    {
        var (userId, vkId) = args;
        var user = await _userRepository.GetUser(userId);
        if (user == null)
        {
            throw new ServiceException("Пользователь не найден", HttpStatusCode.NotFound);
        }

        var isPasswordValid = await _userRepository.CheckPasswordAsync(user, vkId);
        if (!isPasswordValid)
        {
            // Confusing hackers :)
            throw new ServiceException("Ошибка аутентификации", HttpStatusCode.Forbidden);
        }

        var result = await _userRepository.DeleteUser(userId);
        if (!result.Success)
        {
            throw new ServiceException("Неизвестная ошибка при удалении. Обратитесь к администратору", HttpStatusCode.UnprocessableEntity);
        }
    }
}