using System;
using UnityEngine.Networking;
using WebRequests.Observers;

namespace Authentication.Receivers
{
    public interface IAuthenticationReceiver : IObserver<UnityWebRequestAsyncOperation>
    {
        void Subscribe(IWebRequestObservable webRequestObservable);
    }
}