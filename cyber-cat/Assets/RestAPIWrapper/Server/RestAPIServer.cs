using System;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Proyecto26;
using UnityEngine;

namespace RestAPIWrapper.Server
{
    public class RestAPIServer : IRestAPI
    {
        private static async UniTask<string> ServerUrl()
        {
            //var baseServer = Resources.Load<BaseServer>("Base server");
            //_root = await baseServer.GetActualServerURL();

            //return "http://localhost:5000";
            return "https://server.cyber-cat.pro";
        }

        public async UniTask<string> SendCodeToChecking(string token, string taskId, string code, string progLanguage,
            IProgress<float> progress = null)
        {
            var formData = new WWWForm();

            formData.AddField("task_id", taskId);
            formData.AddField("source_text", code);
            formData.AddField("lang", progLanguage);

            var request = new RequestHelper
            {
                Uri = await ServerUrl() + "/solution",
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
                Uri = await ServerUrl() + "/tasks/flat",
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
                Uri = await ServerUrl() + "/tasks/hierarchy",
                Params =
                {
                    ["token"] = token,
                },
                ProgressCallback = value => progress?.Report(value),
                EnableDebug = Debug.isDebugBuild
            };

            var tasks = await RestClient.Get<JObject>(request).ToUniTask();
            return tasks;
        }

        public async UniTask<string> GetLastSavedCode(string token, string taskId,
            IProgress<float> progress = null)
        {
            var request = new RequestHelper
            {
                Uri = await ServerUrl() + "/solution",
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

        public async UniTask<string> GetAuthData(string email, string password,
            IProgress<float> progress = null)
        {
            var request = new RequestHelper
            {
                Uri = await ServerUrl() + "/authentication/login",
                Params =
                {
                    ["email"] = email,
                    ["password"] = password
                },
                ProgressCallback = value => progress?.Report(value),
                EnableDebug = Debug.isDebugBuild
            };

            var response = await RestClient.Get(request).ToUniTask();
            return response.Text;
        }

        public async UniTask RegisterUser(string login, string password, string name)
        {
            throw new NotImplementedException();
        }

        public async UniTask RestorePassword(string login, string password)
        {
            throw new NotImplementedException();
        }
    }
}