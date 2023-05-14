using Newtonsoft.Json;

namespace ServerAPI.InternalDto
{
    internal class TokenSessionDto : ITokenSession
    {
        [JsonProperty("access_token")] public string AccessToken { get; set; }

        public string Value => AccessToken;
    }
}