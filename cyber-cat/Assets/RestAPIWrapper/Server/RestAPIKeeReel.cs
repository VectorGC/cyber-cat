using System;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Proyecto26;
using UnityEngine;

namespace RestAPIWrapper.Server
{
    public class RestAPIKeeReel : IRestAPI
    {
        public async UniTask<string> SendCodeToChecking(string token, string taskId, string code, string progLanguage,
            IProgress<float> progress = null)
        {
            var formData = new WWWForm();

            formData.AddField("task_id", taskId);
            formData.AddField("source_text", code);
            formData.AddField("lang", progLanguage);

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

        public async UniTask<string> GetTasks(string token, IProgress<float> progress = null)
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

            var response = await RestClient.Get(request).ToUniTask();
            return response.Text;
        }

        public async UniTask<JObject> GetTaskFolders(string token, IProgress<float> progress = null)
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

        public async UniTask<string> GetLastSavedCode(string token, string taskId,
            IProgress<float> progress = null)
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

        public async UniTask<string> GetAuthData(string login, string password,
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

            var response = await RestClient.Get(request).ToUniTask();
            return response.Text;
        }

        public async UniTask RegisterUser(string login, string password, string name)
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

            await RestClient.Post(request).ToUniTask();
        }

        public async UniTask RestorePassword(string login, string password)
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

            await RestClient.Post(request).ToUniTask();
        }
    }
}