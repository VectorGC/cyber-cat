using System.Text;
using ApiGateway.Client.Models;
using Bonsai;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

[BonsaiNode("CodeEditor/")]
public class OpenCodeEditor : AsyncTask
{
    [SerializeField] private TaskType _taskType;

    private ITask _task;

    [Inject]
    private async void Construct(AsyncInject<IPlayer> playerAsync)
    {
        var player = await playerAsync;
        _task = player.Tasks[_taskType.GetId()];
    }

    protected override async UniTask<Status> RunAsync()
    {
        await CodeEditor.OpenAsync(_task);
        return Status.Success;
    }

    public override void Description(StringBuilder builder)
    {
        base.Description(builder);
        builder.AppendLine($"Task: {_taskType}");
    }
}