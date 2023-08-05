using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using Shared.Models.Dto;
using Shared.Models.Models;

namespace PlayerService.Repositories.InternalModels;

[CollectionName("Players")]
public class PlayerModel : IPlayer, IDocument<Guid>
{
    public string UserId { get; set; } = "";
    public int CompletedTasksCount { get; set; } = 0;
    public int BitcoinCount { get; set; } = 0;
    
    public Guid Id { get; set; }
    public int Version { get; set; }
    
    public PlayerModel(string userId)
    {
        UserId = userId;
        CompletedTasksCount = 0;
        BitcoinCount = 0;
    }

    public PlayerModel()
    {
        
    }

    public PlayerDto ToDto()
    {
        return new PlayerDto
        {
            UserId = UserId,
            CompletedTasksCount = CompletedTasksCount,
            BitcoinCount = BitcoinCount
        };
    }
}