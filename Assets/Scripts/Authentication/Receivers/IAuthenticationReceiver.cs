using System;
using UnityEngine.Networking;

namespace Authentication.Receivers
{
    public interface IAuthenticationReceiver : IObserver<UnityWebRequestAsyncOperation>
    {
        //void Subscribe(IWebRequestObservable webRequestObservable);
    }
}