using ProtoBuf;

namespace Shared.Models.Dto
{
    [ProtoContract]
    public class TestDto
    {
        [ProtoMember(1)] public string Input { get; set; }
        [ProtoMember(2)] public string ExpectedOutput { get; set; }
    }
}