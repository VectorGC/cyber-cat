using ProtoBuf;
using Shared.Models.Models;

namespace Shared.Models.Dto
{
    [ProtoContract]
    public class PlayerDto : IPlayer
    {
        [ProtoMember(1)]
        public int BitcoinsAmount
        {
            get => bitcoinsAmount; 
            set => bitcoinsAmount = value;
        }

        public int bitcoinsAmount;
    }
}