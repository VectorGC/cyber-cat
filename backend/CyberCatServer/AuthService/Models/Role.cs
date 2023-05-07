using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace AuthService.Models;

[CollectionName("Roles")]
public class Role : MongoIdentityRole
{
}