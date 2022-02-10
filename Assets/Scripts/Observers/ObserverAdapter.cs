using System;

namespace Observers
{
    public class ObserverAdapter<TValueSource, TValueDestination> : IObserver<TValueSource>
    {
        private readonly IObserver<TValueDestination> _destinationObserver;
        private readonly Func<TValueSource, TValueDestination> _convertMethod;

        public ObserverAdapter(IObserver<TValueDestination> destinationObserver,
            Func<TValueSource, TValueDestination> convertMethod)
        {
            _destinationObserver = destinationObserver;
            _convertMethod = convertMethod;
        }

        public void OnCompleted() => _destinationObserver.OnCompleted();

        public void OnError(Exception error) => _destinationObserver.OnError(error);

        public void OnNext(TValueSource value)
        {
            var destinationValue = _convertMethod.Invoke(value);
            _destinationObserver.OnNext(destinationValue);
        }
    }
}