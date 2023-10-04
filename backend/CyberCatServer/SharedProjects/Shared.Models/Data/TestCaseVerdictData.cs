using ProtoBuf;

namespace Shared.Models.Data
{
    [ProtoContract]
    public class TestCaseVerdictData
    {
        [ProtoMember(1)] public object Output { get; set; }
        [ProtoMember(2)] public bool IsSuccess { get; set; }
        [ProtoMember(3)] public string Error { get; set; }
    }
}