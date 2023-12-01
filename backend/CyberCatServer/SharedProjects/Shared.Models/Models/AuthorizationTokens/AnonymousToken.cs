using Shared.Models.Infrastructure.Authorization;

namespace Shared.Models.Models.AuthorizationTokens
{
    public class AnonymousToken : AuthorizationToken
    {
        public override string Type => string.Empty;
        public override string Value => string.Empty;
        public override string TokenName => string.Empty;
    }
}