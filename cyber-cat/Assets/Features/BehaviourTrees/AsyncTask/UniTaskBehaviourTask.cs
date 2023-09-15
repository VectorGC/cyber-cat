using Bonsai.Core;
using Cysharp.Threading.Tasks;

public abstract class UniTaskBehaviourTask : Task
{
    private AsyncTaskBehaviour _asyncTask;

    public override void OnEnter()
    {
        _asyncTask = new AsyncTaskBehaviour(RunAsync);
    }

    public override Status Run()
    {
        return _asyncTask.Run();
    }

    protected abstract UniTask<Status> RunAsync();
}