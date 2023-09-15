using Bonsai.Core;
using Cysharp.Threading.Tasks;

public abstract class AsyncTask : Task
{
    private AsyncTaskBehaviour _asyncTask;

    public override void OnEnter()
    {
        _asyncTask = new AsyncTaskBehaviour(InternalRunAsync);
    }

    public override Status Run()
    {
        return _asyncTask.Run();
    }

    private async UniTask<Status> InternalRunAsync()
    {
        await UniTask.DelayFrame(1);
        return await RunAsync();
    }

    protected abstract UniTask<Status> RunAsync();
}