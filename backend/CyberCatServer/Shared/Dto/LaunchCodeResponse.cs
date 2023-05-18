using ProtoBuf;

namespace Shared.Dto;

[ProtoContract]
public class LaunchCodeResponse
{
    [ProtoMember(1)] public string StandardOutput { get; set; }
    [ProtoMember(2)] public string StandardError { get; set; }
}