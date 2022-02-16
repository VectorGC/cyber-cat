using Observers;

namespace Authentication.Requests
{
    public interface IAuthenticationRequest
    {
        IWebRequestObservable Authenticate(AuthenticationData authenticationData);
    }
}