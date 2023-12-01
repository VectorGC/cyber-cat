using System;
using Shared.Models.Infrastructure.Authorization;

namespace Shared.Models.Models.AuthorizationTokens
{
    public class AuthorizationTokenHolder
    {
        public event Action<AuthorizationToken> TokenChanged;

        public AuthorizationToken Token
        {
            get => _token;
            set
            {
                _token = value;
                TokenChanged?.Invoke(_token);
            }
        }

        private AuthorizationToken _token;
    }
}