using AuthService.Domain;
using Shared.Models.Domain.Users;

namespace AuthService.Infrastructure;

public class UserEntityMapper
{
    public UserModel ToDomain(UserEntity entity)
    {
        return new UserModel()
        {
            Email = entity.Email,
            FirstName = entity.FirstName,
            Roles = new Roles(entity.Roles)
        };
    }
}