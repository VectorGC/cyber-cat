using ApiGateway.Client.Internal.Tasks.Statuses;
using Bonsai;
using Cysharp.Threading.Tasks;
using Features.GameManager;
using Shared.Models.Ids;

[BonsaiNode("Conditional/", "Condition")]
public class IsTaskActive : ConditionalAbortAsync
{
    protected override async UniTask<bool> ConditionAsync()
    {
        var client = await GameConfig.GetOrCreatePlayerClient();
        var task = Blackboard.Get<TaskType>("task");
        var status = await client.Tasks[(TaskId) task.Id()].GetStatus();

        return status is NotStarted || status is HaveSolution;
    }
}