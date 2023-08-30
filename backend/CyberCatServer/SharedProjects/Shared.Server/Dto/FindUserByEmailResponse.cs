using ProtoBuf;
using Shared.Server.Models;

namespace Shared.Server.Dto
{
    [ProtoContract]
    public class FindUserByEmailResponse
    {
        [ProtoMember(1)] public UserId UserId { get; set; }
        public bool IsSucceeded => UserId != null;
    }
}