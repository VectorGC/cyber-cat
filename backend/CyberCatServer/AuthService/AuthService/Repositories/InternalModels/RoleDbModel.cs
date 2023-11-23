using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using Shared.Models.Domain.Users;

namespace AuthService.Repositories.InternalModels;

[CollectionName("Roles")]
internal class RoleDbModel : MongoIdentityRole<string>
{
    public RoleDbModel(Role role)
    {
        Name = role.Id;
    }

    public RoleDbModel()
    {
    }
}