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
    
        public static UniTask<T> ToUniTask<T>(this RSG.IPromise<T> promise, IProgress<float> progress = default)
        {
            var utcs = new UniTaskCompletionSource<T>();
            promise.Done(obj => utcs.TrySetResult(obj), x => utcs.TrySetException(x));

            if (progress != null)
            {
                promise.Progress(progress.Report);
            }

            return utcs.Task;
        }
    }
}