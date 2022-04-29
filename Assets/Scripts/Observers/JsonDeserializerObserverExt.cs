using System;
using UniRx;

namespace Observers
{
    public static class JsonDeserializerObserverExt
    {
        public static IObservable<T> JsonDeserialize<T>(this IObservable<string> observableText)
        {
            var jsonDeserializeObserver = new JsonDeserializerObserver<T>();
            observableText.Subscribe(jsonDeserializeObserver);

            return jsonDeserializeObserver;
        }

        public static IDisposable JsonDeserialize<T>(this IObservable<string> observableText, Action<T> onNext)
        {
            var jsonDeserializeObserver = observableText.JsonDeserialize<T>();
            return jsonDeserializeObserver.Subscribe(onNext);
        }
    }
}