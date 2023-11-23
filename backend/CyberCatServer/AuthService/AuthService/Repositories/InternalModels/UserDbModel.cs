using System.Collections.Generic;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using Shared.Models.Domain.Users;

namespace AuthService.Repositories.InternalModels;

[CollectionName("Users")]
internal sealed class UserDbModel : MongoIdentityUser<string>
{
    public void SetData(User user, string roleId)
    {
        Roles = new List<string>()
        {
            roleId
        };

        Email = user.Email;
        UserName = user.UserName;
    }

    public User ToDomainModel()
    {
        return new User
        {
            Id = new UserId(Id),
            Email = Email,
            UserName = UserName
        };
    }
}