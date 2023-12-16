using System.Text;
using ApiGateway.Client.Application;
using Bonsai;
using Bonsai.Core;
using UnityEngine;
using Zenject;

[BonsaiNode("ServerAPI/Tasks/", "Condition")]
public class IsTaskSolved : ConditionalAbort
{
    [SerializeField] private TaskType _taskType;

    private ApiGatewayClient _client;

    [Inject]
    private void Construct(ApiGatewayClient client)
    {
        _client = client;
    }

    public override bool Condition()
    {
        if (_client.Player == null)
        {
            var verdict = _client.VerdictHistory.GetBestOrLastVerdict(_taskType.Id());
            return verdict?.IsSuccess ?? false;
        }

        var task = _client.Player.Tasks[_taskType.Id()];
        return task.IsComplete;
    }

    public override void Description(StringBuilder builder)
    {
        base.Description(builder);
        builder.AppendLine($"Task: {_taskType}");
    }
}