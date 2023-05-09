using Authentication;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using ServerAPIBase;
using System;
using TaskUnits;

namespace RestAPIWrapper.Serverless
{
    public class WebAPIProxyServerless : WebApiProxy<string, TokenSession, string, string, TokenSession, ICodeConsoleMessage, string, ITaskDataCollection>
    {
        public WebAPIProxyServerless()
        {
            WebApiAggregator = new WebApiAggregatorServerless();
        }

        public override void Authentificate(string login, string password, string email, string token, Action<TokenSession> callback = null)
        {
            IAuthenticatorData data = new AuthenticatorData(login, password, email, token);
            WebApiAggregator.Authenticator.Request(data, x =>
            callback?.Invoke(JsonConvert.DeserializeObject<TokenSession>(x)));
        }

        public async override UniTask<TokenSession> Authentificate(string login, string password, string email, string token)
        {
            IAuthenticatorData data = new AuthenticatorData(login, password, email, token);
            var json = await WebApiAggregator.Authenticator.RequestAsync(data);
            var tokenSession = JsonConvert.DeserializeObject<TokenSession>(json);
            return tokenSession;
        }


        public override void GetTasks(string token, Action<ITaskDataCollection> callback = null)
        {
            ITasksGetterData data = new TasksGetterData(token);
            WebApiAggregator.TasksGetter.Request(data, x =>
            callback?.Invoke(JsonConvert.DeserializeObject<ITaskDataCollection>(x)));
        }

        public async override UniTask<ITaskDataCollection> GetTasks(string token)
        {
            ITasksGetterData data = new TasksGetterData(token);
            var json = await WebApiAggregator.TasksGetter.RequestAsync(data);
            var list = JsonConvert.DeserializeObject<ITaskDataCollection>(json);
            return list;
        }


        public override void GetToken(string login, string password, string email, Action<TokenSession> callback = null, bool local = false)
        {
            ITokenReceiverData data = new TokenReceiverData(login, password, email);
            WebApiAggregator.TokenReceiver.Request(data, x =>
            callback?.Invoke(JsonConvert.DeserializeObject<TokenSession>(x)));
        }

        public async override UniTask<TokenSession> GetToken(string login, string password, string email, bool local = false)
        {
            ITokenReceiverData data = new TokenReceiverData(login, password, email);
            var json = await WebApiAggregator.TokenReceiver.RequestAsync(data);
            var tokenSession = JsonConvert.DeserializeObject<TokenSession>(json);
            return tokenSession;
        }


        public override void ReceiveCode(string token, string taskID, Action<string> callback = null)
        {
            ICodeReceiverData data = new CodeReceiverData(taskID, token);
            WebApiAggregator.CodeReceiver.Request(data, callback);
        }

        public async override UniTask<string> ReceiveCode(string token, string taskID)
        {
            ICodeReceiverData data = new CodeReceiverData(taskID, token);
            return await WebApiAggregator.CodeReceiver.RequestAsync(data);
        }


        public override void Register(string login, string password, Action<string> callback = null)
        {
            IRegistratorData data = new RegistratorData(login, password);
            WebApiAggregator.Registrator.Request(data, callback);
        }

        public async override UniTask<string> Register(string login, string password)
        {
            IRegistratorData data = new RegistratorData(login, password);
            return await WebApiAggregator.Registrator.RequestAsync(data);
        }


        public override void RestorePassword(string login, string password, Action<string> callback = null)
        {
            IPasswordRestorerData data = new PasswordRestorerData(login, password);
            WebApiAggregator.PasswordRestorer.Request(data, callback);
        }

        public async override UniTask<string> RestorePassword(string login, string password)
        {
            IPasswordRestorerData data = new PasswordRestorerData(login, password);
            return await WebApiAggregator.PasswordRestorer.RequestAsync(data);
        }


        public override void SendCode(ITaskData taskData, string token, string code, string progLanguage, Action<ICodeConsoleMessage> callback = null)
        {
            ICodeSenderData data = new CodeSenderData(taskData, token, code, progLanguage);
            WebApiAggregator.CodeSender.Request(data, x => callback?.Invoke(JsonConvert.DeserializeObject<VerdictResult>(x)));
        }

        public async override UniTask<ICodeConsoleMessage> SendCode(ITaskData taskData, string token, string code, string progLanguage)
        {
            return new VerdictError();
        }
    }
}