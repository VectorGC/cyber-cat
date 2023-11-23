using ProtoBuf;

namespace Shared.Models.Infrastructure.Authorization
{
    [ProtoContract(SkipConstructor = true)]
    public class JwtAccessToken : AuthorizationToken
    {
        public override string Type => JwtBearerDefaults.AuthenticationScheme;
        public override string Value => access_token;
        public override string TokenName => "access_token";

        [ProtoMember(1)] public string access_token { get; set; }
        [ProtoMember(2)] public string username { get; set; }

        public JwtAccessToken(string accessToken, string userName)
        {
            access_token = accessToken;
            username = userName;
        }
    }
}