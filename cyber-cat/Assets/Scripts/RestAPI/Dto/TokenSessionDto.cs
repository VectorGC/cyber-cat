using Newtonsoft.Json;

namespace RestAPI.Dto
{
    public class TokenSessionDto : ITokenSession
    {
        [JsonProperty("token")] public string Token { get; set; }

        public string Value => Token;
    }
}