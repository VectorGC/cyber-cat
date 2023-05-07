using Authentication;
using Cysharp.Threading.Tasks;
using Microsoft.Win32;
using Newtonsoft.Json;
using ServerAPIBase;
using System;

namespace RestAPIWrapper.V1
{
    public class WebApiProxyV1 : WebApiProxy<string, TokenSession, string, string, TokenSession>
    {
        public WebApiProxyV1()
        {
            WebApiAggregator = new WebApiAggregatorV1();
        }

        public override void Authentificate(string login, string password, string email, string token, Action<TokenSession> callback = null)
        {
            AuthenticatorData data = new AuthenticatorData(login, password, email, token);
            WebApiAggregator.Authenticator.Request(data, x =>
            callback?.Invoke(JsonConvert.DeserializeObject<TokenSession>(x)));
        }

        public override async UniTask<TokenSession> Authentificate(string login, string password, string email, string token)
        {
            AuthenticatorData data = new AuthenticatorData(login, password, email, token);
            var json = await WebApiAggregator.Authenticator.RequestAsync(data);
            var tokenSession = JsonConvert.DeserializeObject<TokenSession>(json);
            return tokenSession;
        }


        public override void GetToken(string login, string password, string email, Action<TokenSession> callback = null, bool local = false)
        {
            ITokenReceiverData data = new TokenReceiverData(login, password, email);
            if (local)
            {
                (WebApiAggregator as WebApiAggregatorV1).LocalTokenReceiver.Request(data, callback);
            }
            else
            {
                WebApiAggregator.TokenReceiver.Request(data, x =>
                callback?.Invoke(JsonConvert.DeserializeObject<TokenSession>(x)));
            }
        }

        public override async UniTask<TokenSession> GetToken(string login, string password, string email, bool local = false)
        {
            ITokenReceiverData data = new TokenReceiverData(login, password, email);
            if (local)
            {
                return await (WebApiAggregator as WebApiAggregatorV1).LocalTokenReceiver.RequestAsync(data);
            }
            else
            {
                var json =  await WebApiAggregator.TokenReceiver.RequestAsync(data);
                var tokenSession = JsonConvert.DeserializeObject<TokenSession>(json);
                return tokenSession;
            }
        }


        public override void Register(string login, string password, Action<string> callback = null)
        {
            IRegistratorData data = new RegistratorData(login, password);
            WebApiAggregator.Registrator.Request(data, callback);
        }

        public override async UniTask<string> Register(string login, string password)
        {
            IRegistratorData data = new RegistratorData(login, password);
            return await WebApiAggregator.Registrator.RequestAsync(data);
        }


        public override void RestorePassword(string login, string password, Action<string> callback = null)
        {
            IPasswordRestorerData data = new PasswordRestorerData(login, password);
            WebApiAggregator.PasswordRestorer.Request(data, callback);
        }

        public override async UniTask<string> RestorePassword(string login, string password)
        {
            IPasswordRestorerData data = new PasswordRestorerData(login, password);
            return await WebApiAggregator.PasswordRestorer.RequestAsync(data);
        }
    }
}