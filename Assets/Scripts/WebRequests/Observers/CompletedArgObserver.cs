using System;
using Observers;

namespace WebRequests.Observers
{
    public interface ICompletionObserver<in T> : IObserver<T>
    {
        void OnCompleted(T value);
    }

    public class CompletionObservable<T> : IObservable<T>, IObserver<T>
    {
        private readonly ObserversList<T> _observers = new ObserversList<T>();
        
        private T _cachedLastValue;

        public CompletionObservable(IObservable<T> sourceObservable)
        {
            sourceObservable.Subscribe(this);
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return _observers.Subscribe(observer);
        }

        public void OnCompleted()
        {
            _observers.OnCompleted();

            foreach (var observer in _observers)
            {
                if (observer is ICompletionObserver<T> completionObserver)
                {
                    completionObserver.OnCompleted(_cachedLastValue);
                }
            }
        }

        public void OnError(Exception error)
        {
            _observers.OnError(error);
        }

        public void OnNext(T value)
        {
            _cachedLastValue = value;
            _observers.OnNext(value);
        }
    }
}