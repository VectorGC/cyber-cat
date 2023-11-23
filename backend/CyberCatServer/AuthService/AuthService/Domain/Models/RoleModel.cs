using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using Shared.Models.Domain.Users;

namespace AuthService.Domain.Models;

[CollectionName("Roles")]
internal class RoleModel : MongoIdentityRole<string>
{
    public RoleModel(Role role)
    {
        Name = role.Id;
    }

    public RoleModel()
    {
    }
}