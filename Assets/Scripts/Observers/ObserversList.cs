using System;
using System.Collections;
using System.Collections.Generic;

namespace Observers
{
    public class ObserversList<T> : IReadOnlyList<IObserver<T>>, IObservable<T>, IObserver<T>
    {
        private readonly List<IObserver<T>> _observers = new List<IObserver<T>>();

        public IDisposable Subscribe(IObserver<T> observer)
        {
            _observers.Add(observer);
            return new Unsubscriber<T>(_observers, observer);
        }

        public void OnCompleted()
        {
            foreach (var observer in _observers)
            {
                observer.OnCompleted();
            }
        }

        public void OnError(Exception error)
        {
            foreach (var observer in _observers)
            {
                observer.OnError(error);
            }
        }

        public void OnNext(T value)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(value);
            }
        }

        #region IReadOnlyList implementation

        public IEnumerator<IObserver<T>> GetEnumerator()
        {
            return _observers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _observers.Count;

        public IObserver<T> this[int index] => _observers[index];

        #endregion
    }
}