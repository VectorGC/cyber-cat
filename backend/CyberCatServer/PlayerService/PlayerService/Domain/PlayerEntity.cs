using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using Shared.Models.Domain.Users;
using Shared.Models.Domain.Verdicts;

namespace PlayerService.Domain;

[CollectionName("Players")]
public class PlayerEntity : IDocument<long>
{
    public long Id { get; set; }
    public int Version { get; set; }
    public List<VerdictEntity> Verdicts { get; set; } = new List<VerdictEntity>();

    public PlayerEntity(UserId userId)
    {
        Id = userId.Value;
    }

    public PlayerEntity()
    {
    }
}