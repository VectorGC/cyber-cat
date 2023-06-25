using Cysharp.Threading.Tasks;
using Proyecto26;
using Shared.Models.Models;
using UnityEngine;

namespace ServerAPI
{
    public class RestClientServerAPI : IServerAPI
    {
        private string _uri;
        private IServerAPI _serverless = new Serverless();

        public RestClientServerAPI(string uri)
        {
            _uri = uri;
        }

        public async UniTask<ITask> GetTask(string taskId)
        {
            return await _serverless.GetTask(taskId);
        }

        public async UniTask<string> Authenticate(string email, string password)
        {
            var formData = new WWWForm();

            var request = new RequestHelper
            {
                Uri = _uri + "/auth/login",
                Params =
                {
                    ["email"] = email,
                    ["password"] = password
                },
                FormData = formData,
                EnableDebug = Debug.isDebugBuild
            };

            var response = await RestClient.Post(request).ToUniTask();
            return response.Text;
        }

        public async UniTask<string> AuthorizePlayer(string token)
        {
            return await _serverless.AuthorizePlayer(token);
        }

        public void AddAuthorizationToken(string token)
        {
            RestClient.DefaultRequestHeaders["Authorization"] = "Bearer " + token;
            _serverless.AddAuthorizationToken(token);
        }

        public async UniTask<string> GetSavedCode(string taskId)
        {
            return await _serverless.GetSavedCode(taskId);
        }

        public async UniTask<IVerdict> VerifySolution(string taskId, string sourceCode)
        {
            return await _serverless.VerifySolution(taskId, sourceCode);
        }
    }
}