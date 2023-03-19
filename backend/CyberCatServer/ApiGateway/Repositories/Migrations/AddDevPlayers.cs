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
                Email = "Alkon",
                Name = "Иван",
                LastName = "Алкон",
                Password = "1"
            },
            new UserDto()
            {
                Email = "Lukashev",
                Name = "Михаил",
                LastName = "Лукашев",
                Password = "12"
            },
            new UserDto()
            {
                Email = "Karpik",
                Name = "Артем",
                LastName = "Карпинский",
                Password = "123"
            },
            new UserDto()
            {
                Email = "Snegirev",
                Name = "Снегирев",
                LastName = "Святослав",
                Password = "1234"
            },
            new UserDto()
            {
                Email = "Pekush",
                Name = "Даниил",
                LastName = "Пекуш",
                Password = "12345"
            },
            new UserDto()
            {
                Email = "Ustinovskiy",
                Name = "Устиновский",
                LastName = "Георгий",
                Password = "123456"
            },
            new UserDto()
            {
                Email = "Mazhaicev",
                Name = "Евгений",
                LastName = "Мажайцев",
                Password = "1234567"
            },
            new UserDto()
            {
                Email = "Kimsanbaev",
                Name = "Карим",
                LastName = "Кимсанбаев",
                Password = "12345678"
            },
        };

        await repository.Add(users);
    }
}