namespace Shared.Models.Models.AuthorizationTokens
{
    public class AnonymousToken : IAuthorizationToken
    {
        public string Type => string.Empty;
        public string Value => string.Empty;
    }
}