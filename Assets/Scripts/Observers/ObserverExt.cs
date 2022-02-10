using System;

namespace Observers
{
    public static class ObserverExt
    {
        public static IObserver<TValueSource> ToConvertObserver<TValueSource, TValueDestination>(
            this IObserver<TValueDestination> sourceObserver,
            Func<TValueSource, TValueDestination> convertMethod)
        {
            return new ObserverAdapter<TValueSource, TValueDestination>(sourceObserver, convertMethod);
        }
    }
}