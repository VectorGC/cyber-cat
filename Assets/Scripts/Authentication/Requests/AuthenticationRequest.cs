using UnityEngine;

namespace Authentication.Requests
{
    [CreateAssetMenu(fileName = "AuthenticationRequest", menuName = "ScriptableObjects/Authentication/Request",
        order = 1)]
    public class AuthenticationRequest : ScriptableObject//, IAuthenticationRequest
    {
        [SerializeField] private string authenticationRequestQuery =
            "https://kee-reel.com/cyber-cat/login?email={0}%5C&pass={1}";

        // public IWebRequestObservable Authenticate(AuthenticationData authenticationData)
        // {
        //     var query = authenticationData.GetFormattedQuery(authenticationRequestQuery);
        //     Debug.Log($"[AuthenticationRequest] Send query '{query}'");
        //
        //     var webRequest = UnityWebRequest.Get(query);
        //     return null; //webRequest.ToObservable();
        // }
    }
}