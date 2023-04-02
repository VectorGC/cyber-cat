using ApiGateway.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiGateway.Repositories.Models;

[BsonIgnoreExtraElements]
public class UserDbModel : IUser
{
    [BsonId] public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    // Не добавляем это поле в базу, если оно пустое.
    [BsonIgnoreIfDefault] public string? Token { get; set; }

    public UserDbModel(IUser user)
    {
        Email = user.Email;
        Name = user.Name;
        Password = user.Password;
    }

    // У классов БД всегда должен быть конструктор по умолчанию для десериализации.
    public UserDbModel()
    {
    }

    public override string ToString()
    {
        return $"{Email} ({Name})";
    }
}