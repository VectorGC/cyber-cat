using ProtoBuf;
using Shared.Models.Models;
using Shared.Server.Models;

namespace Shared.Server.Dto.Args
{
    [ProtoContract]
    public class GetSavedCodeArgs
    {
        [ProtoMember(1)] public UserId UserId { get; set; }
        [ProtoMember(2)] public TaskId TaskId { get; set; }
    }
}