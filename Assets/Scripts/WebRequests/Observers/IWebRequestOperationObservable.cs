using System;
using UnityEngine.Networking;

namespace WebRequests.Observers
{
    public interface IWebRequestObservable : IObservable<UnityWebRequestAsyncOperation>
    {
        UnityWebRequest WebRequest { get; }
    }
}