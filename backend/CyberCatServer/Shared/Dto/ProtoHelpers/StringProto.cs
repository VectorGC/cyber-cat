using ProtoBuf;

namespace Shared.Dto.ProtoHelpers;

[ProtoContract]
public sealed class StringProto
{
    [ProtoMember(1)] public string Value { get; set; }

    public static implicit operator string(StringProto stringProto)
    {
        return stringProto.Value;
    }

    public static implicit operator StringProto(string value)
    {
        return new StringProto
        {
            Value = value
        };
    }
}