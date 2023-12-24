using System.Text;
using ApiGateway.Client.Application;
using Bonsai;
using Bonsai.Core;
using Shared.Models.Domain.Tasks;
using UnityEngine;
using Zenject;

[BonsaiNode("CodeEditor/")]
public class OpenCodeEditor : Task
{
    [SerializeField] private TaskType _taskType;

    private ICodeEditor _codeEditor;
    private ApiGatewayClient _apiGatewayClient;
    private TaskDescription _task;

    [Inject]
    private async void Construct(ApiGatewayClient apiGatewayClient, ICodeEditor codeEditor)
    {
        _apiGatewayClient = apiGatewayClient;
        _task = await _apiGatewayClient.TaskRepository.GetTaskDescription(_taskType.Id());
        _codeEditor = codeEditor;
    }

    public override void OnEnter()
    {
        _codeEditor.Open(_task);
    }

    public override Status Run()
    {
        if (!_codeEditor.IsOpen)
        {
            return Status.Success;
        }

        return Status.Running;
    }

    public override void Description(StringBuilder builder)
    {
        base.Description(builder);
        builder.AppendLine($"Task: {_taskType}");
    }
}