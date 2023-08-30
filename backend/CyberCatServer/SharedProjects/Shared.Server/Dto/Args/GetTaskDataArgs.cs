using ProtoBuf;
using Shared.Models.Models;
using Shared.Server.Models;

namespace Shared.Server.Dto.Args;

[ProtoContract]
public class GetTaskDataArgs
{
    [ProtoMember(1)] public PlayerId PlayerId { get; set; }
    [ProtoMember(2)] public TaskId TaskId { get; set; }
}