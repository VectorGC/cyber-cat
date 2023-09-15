using System;
using Bonsai.Core;
using Cysharp.Threading.Tasks;

public class AsyncTaskBehaviour
{
    private UniTask<BehaviourNode.Status>? _asyncTask;
    private readonly Func<UniTask<BehaviourNode.Status>> _delegate;

    public AsyncTaskBehaviour(Func<UniTask<BehaviourNode.Status>> @delegate)
    {
        _delegate = @delegate;
        _asyncTask = @delegate();
    }

    public BehaviourNode.Status Run()
    {
        if (!_asyncTask.HasValue)
        {
            return BehaviourNode.Status.Failure;
        }

        switch (_asyncTask.Value.Status)
        {
            case UniTaskStatus.Pending:
                return BehaviourNode.Status.Running;
            case UniTaskStatus.Succeeded:
                var status = _asyncTask.Value.GetAwaiter().GetResult();
                if (status == BehaviourNode.Status.Running)
                {
                    _asyncTask = _delegate();
                    return BehaviourNode.Status.Running;
                }

                return status;
            case UniTaskStatus.Faulted:
            case UniTaskStatus.Canceled:
            default:
                var task = _asyncTask.Value;
                _asyncTask = null;
                task.GetAwaiter().GetResult();
                return BehaviourNode.Status.Failure;
        }
    }
}