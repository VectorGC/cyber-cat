using ProtoBuf;

namespace Shared.Models.Domain.Verdicts
{
    [ProtoContract]
    public class NativeFailure : Verdict
    {
        [ProtoMember(1)] public string Error { get; set; }
    }
}