using Newtonsoft.Json;

namespace Authentication
{
    public struct TokenSession
    {
        [JsonProperty("token")] public string _token;

        [JsonProperty("name")] private string _name;

        [JsonProperty("error")] public string Error { get; set; }

        public string Token => _token;
        public string Name => _name;

        public TokenSession(string token)
        {
            _token = token;
            _name = string.Empty;
            Error = string.Empty;
        }

        public TokenSession(string token, string name)
        {
            _token = token;
            _name = name;
            Error = string.Empty;
        }

        public static implicit operator string(TokenSession tokenSession) => tokenSession.Token;
    }
}