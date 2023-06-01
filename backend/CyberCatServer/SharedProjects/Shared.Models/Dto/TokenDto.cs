using ProtoBuf;

namespace Shared.Dto
{
    [ProtoContract]
    public class TokenDto
    {
        [ProtoMember(1)] public string AccessToken { get; set; }
    }
}