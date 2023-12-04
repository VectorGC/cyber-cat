using ProtoBuf;

namespace Shared.Models.Infrastructure.Authorization
{
    [ProtoContract(SkipConstructor = true)]
    public class VkAccessToken : AuthorizationToken
    {
        public override string Type => JwtAccessToken.Type;

        public override string Value => JwtAccessToken.Value;

        public override string TokenName => JwtAccessToken.TokenName;

        // TODO: Actually, the main VK authorization occurs on the client side. Here, a genuine VK token needs to be created.
        [ProtoMember(1)] public JwtAccessToken JwtAccessToken { get; set; }
    }
}