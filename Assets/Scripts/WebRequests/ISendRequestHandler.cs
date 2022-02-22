using System;

namespace WebRequests
{
    public interface ISendRequestHandler<out T>
    {
        IObservable<T> SendRequest();
    }
}