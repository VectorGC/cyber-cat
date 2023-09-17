using Cysharp.Threading.Tasks;

public abstract class ConditionalAbortAsync : DecoratorAsync
{
    protected override async UniTask<Status> OnEnterAsync()
    {
        var condition = await ConditionAsync();
        if (condition)
        {
            return await base.OnEnterAsync();
        }

        return Status.Failure;
    }

    protected abstract UniTask<bool> ConditionAsync();
}