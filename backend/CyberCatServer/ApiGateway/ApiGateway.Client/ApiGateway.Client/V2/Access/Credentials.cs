using Shared.Models.Models.AuthorizationTokens;

namespace ApiGateway.Client.V2.Access
{
    internal class Credentials : IAccess
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public AuthorizationTokenHolder AuthorizationTokenHolder { get; } = new AuthorizationTokenHolder();
        public bool IsAvailable => true;
    }
}