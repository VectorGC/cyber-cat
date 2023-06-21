using ProtoBuf;

namespace Shared.Models.Dto
{
    [ProtoContract]
    public class TokenDto
    {
        [ProtoMember(1)] public string AccessToken { get; set; }
    }
}