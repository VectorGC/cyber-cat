using System;
using TasksData;

namespace WebRequests
{
    public interface ISendRequestHandler<out T>
    {
        IObservable<T> SendRequest(IProgress<float> progress = null);
    }
}