namespace AuthService.Models;

public interface IUser
{
    string UserName { get; }
    string Email { get; }
}