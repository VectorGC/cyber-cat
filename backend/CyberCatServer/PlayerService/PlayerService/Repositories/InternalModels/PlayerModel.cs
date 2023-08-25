using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using Shared.Models.Dto;
using Shared.Models.Models;

namespace PlayerService.Repositories.InternalModels;

[CollectionName("Players")]
public class PlayerModel : IPlayer, IDocument<string>
{
    public string Id { get; set; }
    public int BitcoinsAmount { get; set; }

    public int Version { get; set; }

    public PlayerDto ToDto()
    {
        return new PlayerDto
        {
            BitcoinsAmount = BitcoinsAmount
        };
    }
}