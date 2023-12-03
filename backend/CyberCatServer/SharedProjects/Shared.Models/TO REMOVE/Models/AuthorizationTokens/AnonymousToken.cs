using Shared.Models.Infrastructure.Authorization;

namespace Shared.Models.TO_REMOVE.Models.AuthorizationTokens
{
    public class AnonymousToken : AuthorizationToken
    {
        public override string Type => string.Empty;
        public override string Value => string.Empty;
        public override string TokenName => string.Empty;
    }
}