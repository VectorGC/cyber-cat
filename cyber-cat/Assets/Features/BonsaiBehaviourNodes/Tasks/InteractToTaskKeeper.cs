using Bonsai;
using Bonsai.Core;
using Cysharp.Threading.Tasks;

[BonsaiNode("Tasks/Interact/", "InteractToTaskKeeper")]
public class InteractToTaskKeeper : Task
{
    private UniTask? _asyncTask;

    public override void OnEnter()
    {
        var player = Blackboard.Get<Player>("player");
        var keeper = Blackboard.Get<TaskKeeper>("task_keeper");

        var canInteract = player.InteractPosibility.CanInteract(keeper);
        if (canInteract)
        {
            _asyncTask = keeper.Interact();
        }
    }

    public override Status Run()
    {
        if (!_asyncTask.HasValue)
        {
            return Status.Failure;
        }

        return _asyncTask.Value.Status == UniTaskStatus.Succeeded ? Status.Success : Status.Running;
    }
}