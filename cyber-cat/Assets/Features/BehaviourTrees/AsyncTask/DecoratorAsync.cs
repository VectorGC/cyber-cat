using Bonsai.Core;
using Cysharp.Threading.Tasks;

public abstract class DecoratorAsync : Decorator
{
    private AsyncTaskBehaviour _asyncTask;

    public override void OnEnter()
    {
        _asyncTask = new AsyncTaskBehaviour(OnEnterAsync);
    }

    public override Status Run()
    {
        return _asyncTask.Run();
    }

    protected virtual async UniTask<Status> OnEnterAsync()
    {
        base.OnEnter();
        return Status.Success;
    }
}