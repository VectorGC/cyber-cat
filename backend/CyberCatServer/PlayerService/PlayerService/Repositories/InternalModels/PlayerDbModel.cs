using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using Shared.Models.Dto;

namespace PlayerService.Repositories.InternalModels;

[CollectionName("Players")]
internal class PlayerDbModel : IDocument<long>
{
    public long Id { get; set; }
    public int BitcoinsAmount { get; set; }
    public Dictionary<string, TaskProgressDbModel> Tasks { get; set; } = new Dictionary<string, TaskProgressDbModel>();

    public int Version { get; set; }

    public PlayerDto ToDto()
    {
        return new PlayerDto
        {
            BitcoinsAmount = BitcoinsAmount
        };
    }
}