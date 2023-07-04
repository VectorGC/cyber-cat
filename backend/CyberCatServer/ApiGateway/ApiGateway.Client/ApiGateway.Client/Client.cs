using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models.Dto;
using Shared.Models.Models;

namespace ApiGateway.Client
{
    public class Client : IClient
    {
        private readonly Uri _uri;
        private readonly IRestClient _restClient;

        public Client(string uri, IRestClient restClient)
        {
            _uri = new Uri(uri);
            _restClient = restClient;
        }

        public void Dispose()
        {
            _restClient.Dispose();
        }

        public async Task<string> Authenticate(string email, string password)
        {
            var form = new Dictionary<string, string>
            {
                ["email"] = email,
                ["password"] = password
            };

            var accessToken = await _restClient.PostAsync(_uri + "auth/login", form);
            return accessToken;
        }

        public async Task<string> AuthorizePlayer(string token)
        {
            AddAuthorizationToken(token);
            var name = await _restClient.GetStringAsync(_uri + "auth/authorize_player");
            RemoveAuthorizationToken();

            return name;
        }

        public async Task<ITask> GetTask(string taskId)
        {
            var task = await _restClient.GetFromJsonAsync<TaskDto>(_uri + $"tasks/{taskId}");
            return task;
        }

        public void AddAuthorizationToken(string token)
        {
            _restClient.AddAuthorizationHeader(JwtBearerDefaults.AuthenticationScheme, token);
        }

        public void RemoveAuthorizationToken()
        {
            _restClient.RemoveAuthorizationHeader();
        }

        public async Task<string> GetSavedCode(string taskId)
        {
            return await _restClient.GetStringAsync(_uri + $"solution/{taskId}");
        }

        public async Task SaveCode(string taskId, string sourceCode)
        {
            await _restClient.PostStringAsync(_uri + $"solution/{taskId}", sourceCode);
        }

        public async Task RemoveSavedCode(string taskId)
        {
            await _restClient.DeleteAsync(_uri + $"solution/{taskId}");
        }

        public async Task<IVerdict> VerifySolution(string taskId, string sourceCode)
        {
            return await _restClient.PostAsJsonAsync<VerdictDto>(_uri + $"judge/verify/{taskId}", sourceCode);
        }

        public async Task<string> GetStringTestAsync()
        {
            //var request = await RestClient.Get("http://localhost:5000/auth/simple").ToUniTask();
            var request = UnityEngine.Networking.UnityWebRequest.Get("http://localhost:5000/auth/simple");

            var tcs = new TaskCompletionSource<string>();
            //tcs.TrySetResult(request.Text);

            var enumerable = request.SendWebRequest();
            enumerable.completed += (obj) =>
            {
                var request2 = (UnityEngine.Networking.UnityWebRequestAsyncOperation) obj;


                var isNetworkError = (request2.webRequest.result == UnityEngine.Networking.UnityWebRequest.Result.ConnectionError);
                var isHttpError = (request2.webRequest.result == UnityEngine.Networking.UnityWebRequest.Result.ProtocolError);

                if (request2.isDone && !isNetworkError && !isHttpError)
                {
                    tcs.TrySetResult(request2.webRequest.downloadHandler.text);
                }
                else
                {
                    tcs.TrySetResult("123");
                }
            };

            return await tcs.Task;
        }
    }
}