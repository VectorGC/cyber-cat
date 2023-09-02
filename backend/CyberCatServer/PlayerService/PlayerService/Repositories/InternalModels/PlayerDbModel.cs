using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using Shared.Models.Dto.Data;

namespace PlayerService.Repositories.InternalModels;

[CollectionName("Players")]
internal class PlayerDbModel : IDocument<long>
{
    public long Id { get; set; }
    public int BitcoinsAmount { get; set; }
    public Dictionary<string, TaskProgressDbModel> Tasks { get; set; } = new Dictionary<string, TaskProgressDbModel>();

    public int Version { get; set; }

    public PlayerData ToDto()
    {
        return new PlayerData
        {
            BitcoinsAmount = BitcoinsAmount
        };
    }
}