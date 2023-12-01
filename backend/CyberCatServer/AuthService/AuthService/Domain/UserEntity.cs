using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace AuthService.Domain;

[CollectionName("Users")]
public sealed class UserEntity : MongoIdentityUser<string>
{
}