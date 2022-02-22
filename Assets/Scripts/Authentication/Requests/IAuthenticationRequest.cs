using System;

namespace Authentication.Requests
{
    public interface IAuthenticationRequest
    {
        IObservable<string> Authenticate(AuthenticationData authenticationData);
    }
}