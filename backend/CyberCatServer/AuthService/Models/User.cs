using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace AuthService.Models;

[CollectionName("Users")]
public sealed class User : MongoIdentityUser, IUser
{
    string IUser.UserName => base.UserName;

    string IUser.Email => base.Email;

    public User(IUser user)
    {
        base.UserName = user.UserName;
        base.Email = user.Email;
    }

    public User()
    {
    }
}