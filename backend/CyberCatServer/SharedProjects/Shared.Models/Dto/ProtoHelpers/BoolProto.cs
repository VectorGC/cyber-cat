using ProtoBuf;

namespace Shared.Models.Dto.ProtoHelpers
{
    [ProtoContract]
    public sealed class BoolProto
    {
        [ProtoMember(1)] public bool Value { get; set; }

        public static implicit operator bool(BoolProto proto)
        {
            return proto.Value;
        }

        public static implicit operator BoolProto(bool value)
        {
            return new BoolProto
            {
                Value = value
            };
        }
    }
}