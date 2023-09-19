using Bonsai.Core;
using Cysharp.Threading.Tasks;

public abstract class AsyncTask : Task
{
    private AsyncTaskBehaviour _asyncTask;
    private bool _isEntered;

    public sealed override void OnEnter()
    {
        _asyncTask = new AsyncTaskBehaviour(InternalRunAsync);
    }

    public sealed override void OnExit()
    {
        _asyncTask = null;
        _isEntered = false;
        base.OnExit();
    }

    public override Status Run()
    {
        return _asyncTask.Run();
    }

    private async UniTask<Status> InternalRunAsync()
    {
        if (!_isEntered)
        {
            _isEntered = true;
            await OnEnterAsync();
        }

        await UniTask.DelayFrame(1);
        var status = await RunAsync();
        await OnExitAsync();

        return status;
    }

    protected virtual UniTask OnEnterAsync()
    {
        return UniTask.CompletedTask;
    }

    protected virtual UniTask OnExitAsync()
    {
        return UniTask.CompletedTask;
    }

    protected abstract UniTask<Status> RunAsync();
}