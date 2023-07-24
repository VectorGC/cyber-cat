using ProtoBuf;
using Shared.Models.Models;

namespace Shared.Models.Dto
{
    [ProtoContract]
    public class PlayerDto : IPlayer
    {
        [ProtoMember(1)] public long UserId { get; set; }
        [ProtoMember(2)] public int CompletedTasksCount { get; set; }
        [ProtoMember(3)] public int BitcoinCount { get; set; }

        public PlayerDto(IPlayer player)
        {
            UserId = player.UserId;
            CompletedTasksCount = player.CompletedTasksCount;
            BitcoinCount = player.BitcoinCount;
        }

        public PlayerDto()
        {
            
        }
    }
}