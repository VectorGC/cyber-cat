using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using Shared.Models.Domain.Users;

namespace PlayerService.Domain;

[CollectionName("Players")]
public class PlayerEntity : IDocument<long>
{
    public long Id { get; set; }
    public Dictionary<string, TaskProgressEntity> Tasks { get; set; } = new Dictionary<string, TaskProgressEntity>();

    public int Version { get; set; }

    public PlayerEntity(UserId userId)
    {
        Id = userId.Value;
    }

    public PlayerEntity()
    {
    }
}