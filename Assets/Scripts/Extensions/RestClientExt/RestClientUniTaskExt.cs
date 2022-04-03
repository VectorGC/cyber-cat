using System;
using Cysharp.Threading.Tasks;
using UniRx;

namespace Extensions.RestClientExt
{
    public static class RestClientUniTaskExt
    {
        public static IObservable<T> ToObservable<T>(this RSG.IPromise<T> promise, IProgress<float> progress = default)
        {
            if (progress != null)
            {
                promise.Progress(progress.Report);
            }

            var observable = Observable.Create<T>(x =>
            {
                promise.Then(x.OnNext, x.OnError).Then(x.OnCompleted);
                return new CancellationDisposable();
            });

            return observable;
        }

        public static UniTask<T> ToUniTask<T>(this RSG.IPromise<T> promise)
        {
            var utcs = new UniTaskCompletionSource<T>();

            promise.Then(obj => utcs.TrySetResult(obj),
                ex => utcs.TrySetException(ex));

            return utcs.Task;
        }
    }
}