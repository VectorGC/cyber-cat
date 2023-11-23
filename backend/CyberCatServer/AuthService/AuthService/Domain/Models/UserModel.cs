using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace AuthService.Domain.Models;

[CollectionName("Users")]
public sealed class UserModel : MongoIdentityUser<string>
{
}