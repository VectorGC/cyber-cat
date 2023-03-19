namespace ApiGateway.Models;

public interface IUser
{
    string Email { get; }
    string Name { get; }
    string LastName { get; }
    string Password { get; }
}