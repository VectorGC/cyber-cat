using System;
using UnityEngine.Networking;

namespace Observers
{
    public interface IWebRequestObservable : IObservable<UnityWebRequestAsyncOperation>
    {
        UnityWebRequest WebRequest { get; }
    }
}