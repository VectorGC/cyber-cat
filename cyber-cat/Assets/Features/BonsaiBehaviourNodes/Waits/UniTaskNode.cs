using Bonsai.Core;
using Cysharp.Threading.Tasks;

public abstract class UniTaskNode : Task
{
    private UniTask<Status>? _asyncTask;

    public override void OnEnter()
    {
        _asyncTask = RunAsync();
    }

    public override Status Run()
    {
        if (!_asyncTask.HasValue)
        {
            return Status.Failure;
        }

        switch (_asyncTask.Value.Status)
        {
            case UniTaskStatus.Pending:
                return Status.Running;
            case UniTaskStatus.Succeeded:
                var status = _asyncTask.Value.GetAwaiter().GetResult();
                if (status == Status.Running)
                {
                    _asyncTask = RunAsync();
                    return Status.Running;
                }

                return status;
            case UniTaskStatus.Faulted:
            case UniTaskStatus.Canceled:
            default:
                var task = _asyncTask.Value;
                _asyncTask = null;
                task.GetAwaiter().GetResult();
                return Status.Failure;
        }
    }

    protected abstract UniTask<Status> RunAsync();
}