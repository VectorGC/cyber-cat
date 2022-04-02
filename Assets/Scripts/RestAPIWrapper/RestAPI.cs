using System;
using Authentication;
using CodeEditorModels.ProgLanguages;
using Cysharp.Threading.Tasks;
using Extensions.RestClientExt;
using Proyecto26;
using UnityEngine;

namespace RestAPIWrapper
{
    public static class RestAPI
    {
        public static async UniTask<TokenSession> GetToken(string login, string password,
            IProgress<float> progress = null)
        {
            var request = new RequestHelper
            {
                Uri = Endpoint.LOGIN,
                Params =
                {
                    ["email"] = login,
                    ["pass"] = password
                }
            };

            return await RestClient.Get<TokenSession>(request).ToUniTask(progress);
        }

        public static async UniTask<TasksData.TasksData> GetTasks(string token, IProgress<float> progress = null)
        {
            var request = new RequestHelper
            {
                Uri = Endpoint.URI,
                Params =
                {
                    ["token"] = token
                }
            };

            return await RestClient.Get<TasksData.TasksData>(request).ToUniTask(progress);
        }

        public static UniTask<ITaskTicket> GetTask(string token, string taskId, IProgress<float> progress = null)
        {
            return new GetTaskRequest(taskId, token).SendRequest(progress);
        }

        public static async UniTask<string> SendCodeToChecking(TokenSession token, int taskId, string code,
            ProgLanguage progLanguage)
        {
            var formData = new WWWForm();

            formData.AddField("task_id", taskId);
            formData.AddField("source_text", code);
            formData.AddField("lang", "c");
            //formData.AddField("verbose", "false");

            //formData.Add(new MultipartFormDataSection("source_text", _codeText));
            //formData.Add(new MultipartFormDataSection("verbose", "false"));

            //var url = new GetTasksRequest(token).GetUri();
            // "https://kee-reel.com/cyber-cat/?" + _token.Token

            //var y = ObservableWWW.Post(Endpoint.URI, formData);
            //y.Do(x => Debug.LogError(y)).Subscribe();

            var request = new RequestHelper
            {
                Uri = Endpoint.URI,
                Params =
                {
                    ["token"] = token
                },
                FormData = formData,
                EnableDebug = true
            };

            var response = await RestClient.Post(request).ToUniTask();
            return response.Text;
        }

        public static async UniTask<TokenSession> Registrate(string login, string password, string name)
        {
            var request = new RequestHelper
            {
                Uri = Endpoint.REGISTER,
                Params =
                {
                    ["email"] = login,
                    ["pass"] = password,
                    ["name"] = name
                }
            };

            return await RestClient.Post<TokenSession>(request).ToUniTask();
        }
    }
}