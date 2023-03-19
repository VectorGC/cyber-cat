using ApiGateway.Models;

namespace ApiGateway.Dto;

public class UserDto : IUser
{
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Password { get; set; } = null!;
}