using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using Shared.Models.Domain.Users;

namespace AuthService.Domain.Models;

[CollectionName("Roles")]
internal class RoleEntity : MongoIdentityRole<string>
{
    public RoleEntity(Role role)
    {
        Name = role.Id;
    }

    public RoleEntity()
    {
    }
}