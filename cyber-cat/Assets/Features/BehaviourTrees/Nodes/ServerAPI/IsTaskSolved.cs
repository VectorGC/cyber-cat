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

    private ApiGatewayClient _apiGatewayClient;

    [Inject]
    private void Construct(ApiGatewayClient apiGatewayClient)
    {
        _apiGatewayClient = apiGatewayClient;
    }

    public override bool Condition()
    {
        if (_apiGatewayClient.Player == null)
            return false;

        var task = _apiGatewayClient.Player.Tasks[_taskType.Id()];
        return task.IsComplete;
    }

    public override void Description(StringBuilder builder)
    {
        base.Description(builder);
        builder.AppendLine($"Task: {_taskType}");
    }
}