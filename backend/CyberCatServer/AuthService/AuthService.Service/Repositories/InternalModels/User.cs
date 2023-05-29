using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using Shared.Models;

namespace AuthService.Service.Repositories.InternalModels;

[CollectionName("Users")]
internal sealed class User : MongoIdentityUser, IUser
{
    public User(IUser user)
    {
        UserName = user.UserName;
        Email = user.Email;
    }

    public User()
    {
    }
}