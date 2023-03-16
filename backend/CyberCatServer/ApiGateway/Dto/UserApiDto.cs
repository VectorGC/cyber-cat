using ApiGateway.Models;

namespace ApiGateway.Dto;

public struct UserApiDto : IUser
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
}