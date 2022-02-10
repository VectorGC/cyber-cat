using System;
using UnityEngine.Networking;

namespace DefaultNamespace.TestButtons
{
    public interface IAuthenticationProvider
    {
        IObservable<UnityWebRequestAsyncOperation> Authenticate(AuthenticationData authenticationData);
    }
}