namespace Shared.Models.Models.AuthorizationTokens
{
    public class VkToken : IAuthorizationToken
    {
        public string Type => "Bearer"; // Vk signed in client side. Server-side use JwtBearer token.
        public string Value { get; set; }
    }
}