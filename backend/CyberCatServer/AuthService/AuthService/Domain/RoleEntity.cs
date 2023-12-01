using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace AuthService.Domain;

[CollectionName("Roles")]
internal class RoleEntity : MongoIdentityRole<string>
{
    public RoleEntity(string roleId)
    {
        Id = roleId;
        Name = roleId;
    }

    public RoleEntity()
    {
    }
}