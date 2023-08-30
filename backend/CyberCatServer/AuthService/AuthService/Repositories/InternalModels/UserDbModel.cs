using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using Shared.Server.Dto;
using Shared.Server.Models;

namespace AuthService.Repositories.InternalModels;

[CollectionName("Users")]
internal sealed class UserDbModel : MongoIdentityUser<long>
{
    public UserDbModel(string userName, string email, IAuthUserRepository repository) : base(userName, email)
    {
        Id = repository.Count + 1;
    }

    public UserDbModel()
    {
    }

    public UserDto ToDto()
    {
        return new UserDto
        {
            Id = new UserId(Id),
            Email = Email,
            UserName = UserName
        };
    }
}