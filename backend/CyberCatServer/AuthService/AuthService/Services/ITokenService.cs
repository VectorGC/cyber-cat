namespace AuthService.Services;

public interface ITokenService
{
    string CreateToken(string email, string userName);
}