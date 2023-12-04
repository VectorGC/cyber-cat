using ProtoBuf;

namespace ApiGateway.Infrastructure.CompleteTaskWebHookService;

[ProtoContract]
public class WebHookResultStatus
{
    [ProtoMember(1)] public string WebHook { get; set; }
    [ProtoMember(2)] public WhoSolvedTaskExternalDto Model { get; set; }
    [ProtoMember(3)] public string Error { get; set; }
}