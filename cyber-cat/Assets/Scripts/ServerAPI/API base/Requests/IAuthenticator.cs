using Cysharp.Threading.Tasks;
using System;

namespace ServerAPIBase
{
    public interface IAuthenticator<T> : IWebApiRequester<IAuthenticatorData, T>
    {

    }

    public interface IAuthenticatorData
    {
        string Login { get; }
        string Password { get; }
        string Email { get; }
        string Token { get; }
    }

    public class AuthenticatorData : IAuthenticatorData
    {
        public string Login { get; }
        public string Password { get; }
        public string Token { get; }
        public string Email { get; }

        public AuthenticatorData(string login, string password, string email, string token = null)
        {
            Login = login;
            Password = password;
            Token = token;
            Email = email;
        }
    }
}