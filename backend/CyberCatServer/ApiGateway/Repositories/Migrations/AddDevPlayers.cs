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
        if (await repository.GetCount() > 0)
        {
            return;
        }

        var users = new List<IUser>
        {
            new UserApiDto()
            {
                Email = "Alkon",
                Name = "Иван",
                LastName = "Алкон",
                Password = "1"
            },
            new UserApiDto()
            {
                Email = "Lukashev",
                Name = "Михаил",
                LastName = "Лукашев",
                Password = "12"
            },
            new UserApiDto()
            {
                Email = "Karpik",
                Name = "Артем",
                LastName = "Карпинский",
                Password = "123"
            },
            new UserApiDto()
            {
                Email = "Snegirev",
                Name = "Снегирев",
                LastName = "Святослав",
                Password = "1234"
            },
            new UserApiDto()
            {
                Email = "Pekush",
                Name = "Даниил",
                LastName = "Пекуш",
                Password = "12345"
            },
            new UserApiDto()
            {
                Email = "Ustinovskiy",
                Name = "Устиновский",
                LastName = "Георгий",
                Password = "123456"
            },
            new UserApiDto()
            {
                Email = "Mazhaicev",
                Name = "Евгений",
                LastName = "Мажайцев",
                Password = "1234567"
            },
            new UserApiDto()
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