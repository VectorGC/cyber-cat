using System.Text;
using ApiGateway.Client.Models;
using Bonsai;
using Bonsai.Core;
using UnityEngine;
using Zenject;

[BonsaiNode("CodeEditor/")]
public class OpenCodeEditor : Task
{
    [SerializeField] private TaskType _taskType;

    private ITask _task;
    private ICodeEditor _codeEditor;

    [Inject]
    private async void Construct(AsyncInject<IPlayer> playerAsync, ICodeEditor codeEditor)
    {
        var player = await playerAsync;
        _task = player.Tasks[_taskType.GetId()];
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