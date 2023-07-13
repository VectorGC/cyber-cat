using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using Shared.Models.Dto;
using Shared.Models.Models;

namespace PlayerService.Repositories.InternalModels;

[CollectionName("Players")]
public class PlayerModel : IPlayer, IDocument<Guid>
{
    public long UserId { get; set; } = 0;
    public string UserName { get; set; } = null!;
    public int CompletedTasksCount { get; set; } = 0;
    
    public Guid Id { get; set; }
    public int Version { get; set; }
    
    public PlayerModel(long userId)
    {
        UserId = userId;
        UserName = "New Player";
        CompletedTasksCount = 0;
    }

    public PlayerModel()
    {
        
    }

    public PlayerDto ToDto()
    {
        return new PlayerDto
        {
            UserId = UserId,
            UserName = UserName,
            CompletedTasksCount = CompletedTasksCount
        };
    }
}