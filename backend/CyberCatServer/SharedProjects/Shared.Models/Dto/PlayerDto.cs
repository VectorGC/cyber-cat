using ProtoBuf;

namespace Shared.Models.Dto
{
    [ProtoContract]
    public class PlayerDto
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