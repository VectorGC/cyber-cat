using ProtoBuf;

namespace Shared.Models.Models.Verdicts
{
    [ProtoInclude(100, typeof(Success))]
    [ProtoInclude(101, typeof(Failure))]
    [ProtoInclude(102, typeof(NativeFailure))]
    [ProtoContract]
    public abstract class Verdict
    {
    }
}