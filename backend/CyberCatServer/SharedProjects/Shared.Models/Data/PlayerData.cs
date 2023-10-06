using ProtoBuf;

namespace Shared.Models.Data
{
    [ProtoContract]
    public class PlayerData
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