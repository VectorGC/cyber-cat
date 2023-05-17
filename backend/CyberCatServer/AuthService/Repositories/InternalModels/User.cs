using AspNetCore.Identity.MongoDbCore.Models;
using AuthService.Models;
using MongoDbGenericRepository.Attributes;

namespace AuthService.Repositories.InternalModels;

[CollectionName("Users")]
internal sealed class User : MongoIdentityUser, IUser
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