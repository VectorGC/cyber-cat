using System.Net;
using System.Threading.Tasks;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure.Authorization;
using Shared.Server.Exceptions;
using Shared.Server.ProtoHelpers;
using Shared.Server.Services;

namespace AuthService.Application;

public class AuthGrpcService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public AuthGrpcService(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<Response<UserId>> CreateUser(CreateUserArgs args)
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

    public async Task<Response> RemoveUser(RemoveUserArgs args)
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
            throw new ServiceException("Неизвестная ошибка при регистрации. Обратитесь к администратору", HttpStatusCode.UnprocessableEntity);
        }

        return new Response();
    }

    public async Task<Response<UserId>> FindByEmail(Args<string> email)
    {
        var user = await _userRepository.FindByEmailAsync(email);
        if (user == null)
            return null;
        return new UserId(user.Id);
    }

    public async Task<Response<AuthorizationToken>> GetAccessToken(GetAccessTokenArgs args)
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
}