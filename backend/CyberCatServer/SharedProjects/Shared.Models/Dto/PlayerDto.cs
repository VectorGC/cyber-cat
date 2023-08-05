using ProtoBuf;
using Shared.Models.Models;

namespace Shared.Models.Dto
{
    [ProtoContract]
    public class PlayerDto : IPlayer
    {
        [ProtoMember(1)]
        public string UserId
        {
            get => userId;
            set => userId = value;
        }
        [ProtoMember(2)] 
        public int CompletedTasksCount 
        { 
            get => completedTasksCount;
            set => completedTasksCount = value; 
        }

        [ProtoMember(3)]
        public int BitcoinCount
        {
            get => bitcoinCount; 
            set => bitcoinCount = value;
        }

        public string userId;
        public int completedTasksCount;
        public int bitcoinCount;

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