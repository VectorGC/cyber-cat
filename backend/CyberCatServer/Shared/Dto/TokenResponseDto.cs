using System.Text.Json.Serialization;
using ProtoBuf;

namespace Shared.Dto;

[ProtoContract]
public class TokenResponseDto
{
    [ProtoMember(1)]
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
}