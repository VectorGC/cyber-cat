using ProtoBuf;
using Shared.Models.Domain.Tasks;

namespace Shared.Models.Domain.Verdicts
{
    [ProtoInclude(100, typeof(Success))]
    [ProtoInclude(101, typeof(Failure))]
    [ProtoInclude(102, typeof(NativeFailure))]
    [ProtoContract]
    public abstract class Verdict
    {
        [ProtoMember(1)] public TaskId TaskId { get; set; }
        [ProtoMember(2)] public string Solution { get; set; }
    }
}