using ProtoBuf;
using Shared.Models.Models;

namespace Shared.Models.Dto
{
    [ProtoContract]
    public class PlayerDto : IPlayer
    {
        [ProtoMember(1)] public long UserId { get; set; }
        [ProtoMember(2)] public string UserName { get; set; }
        [ProtoMember(3)] public int CompletedTasksCount { get; set; }

        public PlayerDto(IPlayer player)
        {
            UserId = player.UserId;
            UserName = player.UserName;
            CompletedTasksCount = player.CompletedTasksCount;
        }

        public PlayerDto()
        {
            
        }
    }
}