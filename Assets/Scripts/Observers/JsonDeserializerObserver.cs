using System;
using Newtonsoft.Json;
using UniRx;

namespace Observers
{
    public class JsonDeserializerObserver<T> : IObserver<string>, IObservable<T>
    {
        private readonly Subject<T> _subject = new Subject<T>();

        public void OnCompleted() => _subject.OnCompleted();

        public void OnError(Exception error) => _subject.OnError(error);

        public void OnNext(string value)
        {
            var obj = JsonConvert.DeserializeObject<T>(value);
            _subject.OnNext(obj);
        }

        public IDisposable Subscribe(IObserver<T> observer) => _subject.Subscribe(observer);
    }
}