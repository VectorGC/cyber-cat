using System.Collections.Generic;
using ProtoBuf;

namespace Shared.Models.Infrastructure.Authorization
{
    [ProtoContract]
    public class JwtAccessToken : AuthorizationToken
    {
        public const string AuthenticationScheme = "Bearer";

        public override string Type => AuthenticationScheme;
        public override string Value => access_token;
        public override string TokenName => "access_token";

        [ProtoMember(1)] public string access_token { get; set; }
        [ProtoMember(2)] public string email { get; set; }
        [ProtoMember(3)] public string firstname { get; set; }
        [ProtoMember(4)] public List<string> roles { get; set; }
    }
}