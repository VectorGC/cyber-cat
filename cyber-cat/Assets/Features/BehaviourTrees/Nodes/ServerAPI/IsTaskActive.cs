using System.Text;
using ApiGateway.Client.Internal.Tasks.Statuses;
using ApiGateway.Client.Models;
using Bonsai;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

[BonsaiNode("ServerAPI/Tasks/", "Condition")]
public class IsTaskActive : ConditionalAbortAsync
{
    [SerializeField] private TaskType _taskType;

    private ITask _task;

    [Inject]
    private async void Construct(AsyncInject<IPlayer> playerAsync)
    {
        var player = await playerAsync;
        _task = player.Tasks[_taskType.GetId()];
    }

    protected override async UniTask<bool> ConditionAsync()
    {
        var status = await _task.GetStatus();
        return status is NotStarted || status is HaveSolution;
    }

    public override void Description(StringBuilder builder)
    {
        base.Description(builder);
        builder.AppendLine($"Task: {_taskType}");
    }
}