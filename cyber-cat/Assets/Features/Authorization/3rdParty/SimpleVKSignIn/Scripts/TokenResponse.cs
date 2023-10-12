using System;

namespace Assets.SimpleVKSignIn.Scripts
{
    /// <summary>
    /// Response specification: https://dev.vk.com/ru/api/access-token/authcode-flow-user
    /// </summary>
    [Serializable]
    public class TokenResponse
    {
        public string access_token;
        public int expires_in;
        public string user_id;
        public string email;
    }
}