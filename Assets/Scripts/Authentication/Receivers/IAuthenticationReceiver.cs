using System;
using Observers;
using UnityEngine.Networking;

public interface IAuthenticationReceiver : IObserver<UnityWebRequestAsyncOperation>
{
    void Subscribe(IWebRequestObservable webRequestObservable);
}