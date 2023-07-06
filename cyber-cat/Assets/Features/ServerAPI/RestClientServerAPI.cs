using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Proyecto26;
using Shared.Models.Dto;
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

        public async UniTask<TaskDto> GetTask(string taskId)
        {
            var request = new RequestHelper
            {
                Uri = _uri + $"/tasks/{taskId}",
                EnableDebug = Debug.isDebugBuild
            };

            var tasks = await RestClient.Get<TaskDto>(request).ToUniTask();
            return tasks;
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
            var request = new RequestHelper
            {
                Uri = _uri + "/solution",
                Params =
                {
                    ["task_id"] = taskId
                },
                EnableDebug = Debug.isDebugBuild
            };

            var response = await RestClient.Get(request).ToUniTask();
            return response.Text;
        }

        public async UniTask<IVerdict> VerifySolution(string taskId, string sourceCode)
        {
            var jsonObject = JsonConvert.SerializeObject(sourceCode);
            var request = new RequestHelper
            {
                Uri = _uri + $"/judge/verify/{taskId}",
                BodyString = jsonObject,
                EnableDebug = Debug.isDebugBuild
            };

            var verdict = await RestClient.Post<VerdictDto2>(request).ToUniTask();
            return verdict;
        }
    }

    public class VerdictDto2 : IVerdict
    {
        public VerdictStatus Status { get; set; }

        public string Error { get; set; }

        public int TestsPassed { get; set; }
    }
}