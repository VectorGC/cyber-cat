using Newtonsoft.Json;

namespace ServerAPI.InternalDto
{
    internal class AuthorizePlayerResponseDto
    {
        [JsonProperty("name")] public string Name { get; set; }
    }
}