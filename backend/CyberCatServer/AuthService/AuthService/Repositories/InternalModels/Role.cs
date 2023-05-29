using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace AuthServiceService.Repositories.InternalModels;

[CollectionName("Roles")]
internal class Role : MongoIdentityRole
{
}