namespace ApiGateway.Models;

public interface IUser
{
    string Email { get; }
    string Name { get; }
    string Password { get; }
}