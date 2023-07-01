using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace AuthService.Repositories.InternalModels;

[CollectionName("Roles")]
internal class Role : MongoIdentityRole
{
}