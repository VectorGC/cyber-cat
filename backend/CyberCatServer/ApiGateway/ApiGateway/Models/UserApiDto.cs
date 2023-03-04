using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiGateway.Controllers;

public interface IUserDto
{
    public string Name { get; }
    public string LastName { get; }
    public string Password { get; }
}

public class UserApiDto : IUserDto
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }


    public static UserApiDto FromUserDto(IUserDto userDto)
    {
        return new UserApiDto()
        {
            Name = userDto.Name,
            LastName = userDto.LastName,
            Password = userDto.Password,
        };
    }
}

[BsonIgnoreExtraElements]
public class UserDbDto : IUserDto
{
    [BsonId]
    [BsonRepresentation(BsonType.Int64)]
    public long Id { get; set; }

    public string Name { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }

    public UserDbDto(IUserDto user)
    {
        Name = user.Name;
        LastName = user.LastName;
        Password = user.Password;
    }
}