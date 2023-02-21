using Cysharp.Threading.Tasks;

public static class RestClientUniExt
    {
        public static UniTask<T> ToUniTask<T>(this RSG.IPromise<T> promise)
        {
            var utcs = new UniTaskCompletionSource<T>();

            promise.Then(obj => utcs.TrySetResult(obj),
                ex => utcs.TrySetException(ex));

            return utcs.Task;
        }
    }