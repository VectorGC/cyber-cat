using ProtoBuf;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Users;

namespace Shared.Server.Domain;

[ProtoContract]
public class SharedTaskProgress
{
    [ProtoMember(1)] public TaskId TaskId { get; set; }
    [ProtoMember(2)] public UserId UserId { get; set; }
    [ProtoMember(3)] public SharedTaskStatus Status { get; set; }
}