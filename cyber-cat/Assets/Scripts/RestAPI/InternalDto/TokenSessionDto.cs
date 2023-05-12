using Newtonsoft.Json;

namespace RestAPI.InternalDto
{
    internal class TokenSessionDto : ITokenSession
    {
        [JsonProperty("token")] public string Token { get; set; }

        public string Value => Token;
    }
}