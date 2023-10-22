using System;
using System.Threading.Tasks;

namespace ApiGateway.Client.V2
{
    public class VK : IAccess
    {
        public string Email { get; private set; }
        public string Name { get; private set; }
        public string Token { get; private set; }

        public bool IsAvailable => true;
        public bool IsSignedIn => !string.IsNullOrEmpty(Token);

        private readonly WebClientAccess _webClientAccess;
        private readonly Credentials _credentials;

        public VK(WebClientAccess webClientAccess, Credentials credentials)
        {
            _credentials = credentials;
            _webClientAccess = webClientAccess;
        }

        public async Task SignIn(string email, string userName)
        {
            Token = await _webClientAccess.SignWithOAuth(email, userName);
            if (string.IsNullOrEmpty(Token))
            {
                return;
            }

            Email = email;
            Name = userName;

            _credentials.Email = email;
            _credentials.Name = Name;
            _credentials.Token = Token;
        }

        public void SignOut()
        {
            Email = string.Empty;
            Name = string.Empty;
            Token = string.Empty;

            _credentials.Email = string.Empty;
            _credentials.Name = string.Empty;
            _credentials.Token = string.Empty;
        }

        public void Dispose()
        {
        }
    }
}