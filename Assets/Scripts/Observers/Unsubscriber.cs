using System;
using System.Collections.Generic;

namespace Observers
{
    /// <summary>
    /// https://docs.microsoft.com/ru-ru/dotnet/standard/events/observer-design-pattern
    /// </summary>
    public class Unsubscriber<T> : IDisposable
    {
        private readonly List<IObserver<T>> _observers;
        private readonly IObserver<T> _observer;

        public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
            {
                _observers.Remove(_observer);
            }
        }
    }
}