using ProtoBuf;

namespace Shared.Models.Infrastructure.Authorization
{
    [ProtoContract]
    [ProtoInclude(100, typeof(JwtAccessToken))]
    [ProtoInclude(101, typeof(VkAccessToken))]
    public abstract class AuthorizationToken
    {
        public abstract string Type { get; }
        public abstract string Value { get; }
        public abstract string TokenName { get; }
    }
}