using System;
using System.Threading.Tasks;
using Authentication;
using CodeEditorModels.ProgLanguages;
using Cysharp.Threading.Tasks;
using Extensions.RestClientExt;
using Newtonsoft.Json.Linq;
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
                },
                ProgressCallback = value => progress?.Report(value),
                EnableDebug = Debug.isDebugBuild
            };

            return await RestClient.Get<TokenSession>(request).ToUniTask();
        }

        public static async UniTask<TasksData.TasksData> GetTasks(string token, IProgress<float> progress = null)
        {
            var request = new RequestHelper
            {
                Uri = Endpoint.TASKS_FLAT,
                Params =
                {
                    ["token"] = token
                },
                ProgressCallback = value => progress?.Report(value),
                EnableDebug = Debug.isDebugBuild
            };

            return await RestClient.Get<TasksData.TasksData>(request).ToUniTask();
        }

        public static async UniTask<string> SendCodeToChecking(TokenSession token, string taskId, string code,
            ProgLanguage progLanguage, IProgress<float> progress = null)
        {
            var formData = new WWWForm();

            formData.AddField("task_id", taskId);
            formData.AddField("source_text", code);
            formData.AddField("lang", RequestParam.ProgLanguages[progLanguage]);

            var request = new RequestHelper
            {
                Uri = Endpoint.SOLUTION,
                Params =
                {
                    ["token"] = token
                },
                FormData = formData,
                ProgressCallback = value => progress?.Report(value),
                EnableDebug = true
            };

            var response = await RestClient.Post(request).ToUniTask();
            return response.Text;
        }

        public static async UniTask<string> GetLastSavedCode(string token, string taskId, IProgress<float> progress = null)
        {
            var request = new RequestHelper
            {
                Uri = Endpoint.SOLUTION,
                Params =
                {
                    ["token"] = token,
                    ["task_id"] = taskId
                },
                ProgressCallback = value => progress?.Report(value),
                EnableDebug = true
            };

            var jObj = await RestClient.Get<JObject>(request).ToUniTask();
            var code = jObj["text"].ToString();

            return code;
        }

        public static async UniTask<TokenSession> RegisterUser(string login, string password, string name)
        {
            var request = new RequestHelper
            {
                Uri = Endpoint.REGISTER,
                Params =
                {
                    ["email"] = login,
                    ["pass"] = password,
                    ["name"] = name
                },
                EnableDebug = Debug.isDebugBuild
            };

            return await RestClient.Post<TokenSession>(request).ToUniTask();
        }

        public static async UniTask<TokenSession> RestorePassword(string login, string password)
        {
            var request = new RequestHelper
            {
                Uri = Endpoint.RESTORE,
                Params =
                {
                    ["email"] = login,
                    ["pass"] = password,
                },
                EnableDebug = Debug.isDebugBuild
            };

            return await RestClient.Post<TokenSession>(request).ToUniTask();
        }

        public static async Task<JObject> GetTaskFolders(string token, IProgress<float> progress = null)
        {
            var request = new RequestHelper
            {
                Uri = Endpoint.TASKS_HIERARCHY,
                Params =
                {
                    ["token"] = token,
                },
                ProgressCallback = value => progress?.Report(value),
                EnableDebug = Debug.isDebugBuild
            };

            return await RestClient.Get<JObject>(request).ToUniTask();
        }
    }
}