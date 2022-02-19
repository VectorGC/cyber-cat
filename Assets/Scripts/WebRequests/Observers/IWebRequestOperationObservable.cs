using System;
using UnityEngine.Networking;

namespace Observers.WebRequest
{
    public interface IWebRequestObservable : IObservable<UnityWebRequestAsyncOperation>
    {
        UnityWebRequest WebRequest { get; }
    }
}