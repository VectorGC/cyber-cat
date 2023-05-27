using System.Text.Json.Serialization;
using ProtoBuf;

namespace Shared.Dto;

[ProtoContract]
public class TokenDto
{
    [ProtoMember(1)]
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
}