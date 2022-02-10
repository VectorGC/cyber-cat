using System;
using DefaultNamespace.TestButtons;
using Observers;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "AuthenticationService", menuName = "ScriptableObjects/Authentication/Service", order = 1)]
public class AuthenticationService : ScriptableObject, IAuthenticationProvider
{
    public string AuthenticationRequestQuery =
        "https://kee-reel.com/cyber-cat/login?email={0}%5C&pass={1}";

    public IObservable<UnityWebRequestAsyncOperation> Authenticate(AuthenticationData authenticationData)
    {
        var query = authenticationData.GetFormattedQuery(AuthenticationRequestQuery);

        var webRequest = UnityWebRequest.Get(query);
        var asyncOperation = webRequest.SendWebRequest();

        var observable = asyncOperation.ToObservable();

        return observable;
    }
}