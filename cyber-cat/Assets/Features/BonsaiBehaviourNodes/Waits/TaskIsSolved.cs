using ApiGateway.Client.Internal.Tasks.Statuses;
using Bonsai;
using Cysharp.Threading.Tasks;
using Features.GameManager;
using Shared.Models.Ids;

[BonsaiNode("Conditional/")]
public class TaskIsSolved : UniTaskNode
{
    protected override async UniTask<Status> RunAsync()
    {
        var client = await GameConfig.GetOrCreatePlayerClient();
        var task = Blackboard.Get<TaskType>("task");
        var status = await client.Tasks[(TaskId) task.Id()].GetStatus();
        if (status is Complete)
        {
            return Status.Success;
        }

        return Status.Failure;
    }
}