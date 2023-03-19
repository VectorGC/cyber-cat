using ApiGateway.Dto;
using ApiGateway.Models;

namespace ApiGateway.Repositories.Migrations;

public interface IUserRepositoryMigration
{
    Task Apply(IUserRepository repository);
}

public class AddDevPlayers : IUserRepositoryMigration
{
    public async Task Apply(IUserRepository repository)
    {
        if (await repository.GetEstimatedCount() > 0)
        {
            return;
        }

        var users = new List<IUser>
        {
            new UserDto()
            {
                Email = "Kimsanbaev",
                Name = "Карим",
                LastName = "Кимсанбаев",
                Password = "123456"
            },
        };

        await repository.Add(users);
    }
}