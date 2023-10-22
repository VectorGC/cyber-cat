using System;

namespace Shared.Models.Models.AuthorizationTokens
{
    public class AuthorizationTokenHolder
    {
        public event Action<IAuthorizationToken> TokenChanged;

        public IAuthorizationToken Token
        {
            get => _token;
            set
            {
                _token = value;
                TokenChanged?.Invoke(_token);
            }
        }

        private IAuthorizationToken _token;
    }
}