using System;
using Authentication;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Proyecto26;
using UniRx;
using UnityEngine;
using ServerAPIBase;

namespace RestAPIWrapper.Serverless
{
    public class RestAPIServerless : IRestAPI
    {
        private const string TasksFlatJson = "tasks_flat.json";
        private const string TasksHierarchyJson = "tasks_hierarchy.json";

        private string _taskFlat;
        private string _taskHierarchy;

        public UniTask<string> SendCodeToChecking(string token, string taskId, string code, string progLanguage,
            IProgress<float> progress = null)
        {
            return UniTask.FromResult(
                "{\"error\":\"Это режим 'без сервера', чтобы тесировать задачи - подключите сервер и уберите директиву SERVERLESS при сборке проекта\"}");
        }

        public async UniTask<string> GetTasks(string token, IProgress<float> progress = null)
        {
            if (string.IsNullOrEmpty(_taskFlat))
            {
                _taskFlat = await ReadFileContent(TasksFlatJson);
            }

            return _taskFlat;
        }

        public async UniTask<JObject> GetTaskFolders(string token, IProgress<float> progress = null)
        {
            if (string.IsNullOrEmpty(_taskHierarchy))
            {
                _taskHierarchy = await ReadFileContent(TasksHierarchyJson);
            }

            return JsonConvert.DeserializeObject<JObject>(_taskHierarchy);
        }

        public UniTask<string> GetLastSavedCode(string token, string taskId, IProgress<float> progress = null)
        {
            const string messageText =
                "Это режим 'без сервера', чтобы активировать загрузку последнего сохраненного кода - пожалуйста включите сервер";
            var message = new CodeConsoleRestAPIMessage(messageText, LogMessageType.Error);

            MessageBroker.Default.Publish<ICodeConsoleMessage>(message);

            return UniTask.FromResult(string.Empty);
        }

        public UniTask<string> GetAuthData(string email, string password, IProgress<float> progress = null)
        {
            var token = new TokenSession("serverless_token", "Cyber Cat");
            return UniTask.FromResult(JsonConvert.SerializeObject(token));
        }

        public UniTask RegisterUser(string login, string password, string name)
        {
            return UniTask.CompletedTask;
        }

        public UniTask RestorePassword(string login, string password)
        {
            return UniTask.CompletedTask;
        }

        private async UniTask<string> ReadFileContent(string fileName)
        {
            var filePath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);
            if (filePath.Contains("://"))
            {
                var response = await RestClient.Get(filePath).ToUniTask();
                return response.Text;
            }

            return System.IO.File.ReadAllText(filePath);
        }
    }
}