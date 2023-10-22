using Shared.Models.Models.AuthorizationTokens;

namespace ApiGateway.Client.V3
{
    internal class CredentialsV3 : IAccessV3
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public AuthorizationTokenHolder AuthorizationTokenHolder { get; } = new AuthorizationTokenHolder();
        public bool IsAvailable => true;
    }
}