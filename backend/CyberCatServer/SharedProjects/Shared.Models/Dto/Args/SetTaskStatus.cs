using ProtoBuf;
using Shared.Models.Enums;
using Shared.Models.Models;

namespace Shared.Models.Dto.Args
{
    [ProtoContract]
    public class SetTaskStatus
    {
        [ProtoMember(1)] public TaskId TaskId { get; set; }
        [ProtoMember(2)] public TaskProgressStatus Status { get; set; }
    }
}