using ProtoBuf;

namespace Shared.Server.Infrastructure;

[ProtoContract]
public class WebHookResultStatus
{
    [ProtoMember(1)] public string WebHook { get; set; }
    [ProtoMember(2)] public SharedTaskExternalDto Model { get; set; }
    [ProtoMember(3)] public string Error { get; set; }
}