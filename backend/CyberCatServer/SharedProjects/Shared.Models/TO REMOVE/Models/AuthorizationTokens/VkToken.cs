using Shared.Models.Infrastructure.Authorization;

namespace Shared.Models.TO_REMOVE.Models.AuthorizationTokens
{
    public class VkToken : AuthorizationToken
    {
        public override string Type => "Bearer"; // Vk signed in client side. Server-side use JwtBearer token.
        public override string Value { get; }
        public override string TokenName => "vk_api_token";

        public VkToken(string value)
        {
            Value = value;
        }
    }
}