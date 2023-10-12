using System;

namespace Assets.SimpleVKSignIn.Scripts
{
    [Serializable]
    public class SavedAuth
    {
        public string ClientId;
        public TokenResponse TokenResponse;
        public long Expiration;
        public UserInfo UserInfo;

        public DateTime ExpirationTime => DateTimeOffset.FromUnixTimeSeconds(Expiration).UtcDateTime;

        public SavedAuth(string clientId, TokenResponse tokenResponse)
        {
            ClientId = clientId;
            TokenResponse = tokenResponse;
            Expiration = DateTimeOffset.UtcNow.AddSeconds(tokenResponse.expires_in).ToUnixTimeSeconds() - 10;
        }
    }
}