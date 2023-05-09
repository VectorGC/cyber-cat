using Authentication;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using ServerAPIBase;
using System;
using TaskUnits;

namespace RestAPIWrapper.Serverless
{
    public class RestAPIServerless : IRestAPIWrapper
    {
        private WebAPIProxyServerless WebApiProxy { get; set; } = new WebAPIProxyServerless();

        public async UniTask<TokenSession> GetAuthData(string email, string password, IProgress<float> progress = null)
        {
            return await WebApiProxy.Authentificate(email, password, email, string.Empty);
        }

        public async UniTask<string> GetLastSavedCode(string token, string taskId, IProgress<float> progress = null)
        {
            return await WebApiProxy.ReceiveCode(token, taskId);
        }

        public async UniTask<JObject> GetTaskFolders(string token, IProgress<float> progress = null)
        {
            throw new NotImplementedException();
        }

        public async UniTask<ITaskDataCollection> GetTasks(string token, IProgress<float> progress = null)
        {
            return await WebApiProxy.GetTasks(token);
        }

        public async UniTask<string> RegisterUser(string login, string password, string name)
        {
            return await WebApiProxy.Register(login, password);
        }

        public async UniTask<string> RestorePassword(string login, string password)
        {
            return await WebApiProxy.RestorePassword(login, password);
        }

        public async UniTask<ICodeConsoleMessage> SendCodeToChecking(string token, string taskId, string code, string progLanguage, IProgress<float> progress = null)
        {
            ITaskData data = new TaskData(taskId, string.Empty, string.Empty, string.Empty, null, 0, 0);
            return await WebApiProxy.SendCode(data, token, code, progLanguage);
        }
    }
}