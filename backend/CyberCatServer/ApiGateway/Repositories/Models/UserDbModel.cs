using ApiGateway.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiGateway.Repositories.Models;

[BsonIgnoreExtraElements]
public class UserDbModel : IUser
{
    [BsonId] public ObjectId Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    // Не добавляем это поле в базу, если оно пустое.
    [BsonIgnoreIfDefault] public string? Token { get; set; }

    public UserDbModel(IUser user)
    {
        Id = ObjectId.GenerateNewId(DateTime.Now);
        Email = user.Email;
        Name = user.Name;
        LastName = user.LastName;
        Password = user.Password;
    }

    // У классов БД всегда должен быть конструктор по умолчанию для десериализации.
    public UserDbModel()
    {
    }

    public override string ToString()
    {
        return $"{Name}, {LastName} ({Email})";
    }
}