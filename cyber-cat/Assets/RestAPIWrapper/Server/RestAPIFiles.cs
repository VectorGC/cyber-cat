using System;
using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Proyecto26;
using RestAPIWrapper.Serverless;
using UnityEngine;

namespace RestAPIWrapper.Server
{
    public class RestAPIFiles : IRestAPI
    {
        private readonly string _savedCodeFolder = Path.Combine(Application.persistentDataPath);
        private readonly IRestAPI _serverless = new RestAPIServerless();

        public async UniTask<string> SendCodeToChecking(string token, string taskId, string code, string progLanguage, IProgress<float> progress = null)
        {
            // Сохраняем код в файл с номером задачи.
            var filePath = Path.Combine(_savedCodeFolder, $"task_{taskId}.txt");
            using (var stream = new StreamWriter(filePath))
            {
                await stream.WriteAsync(code);
            }

            // Пустой объект, без ошибок. Это VerdictResult.
            return "{}";
        }

        public async UniTask<string> GetLastSavedCode(string token, string taskId, IProgress<float> progress = null)
        {
            var filePath = Path.Combine(_savedCodeFolder, $"task_{taskId}.txt");
            if (!File.Exists(filePath))
            {
                return string.Empty;
            }

            using (var stream = new StreamReader(filePath))
            {
                var code = await stream.ReadToEndAsync();
                return code;
            }
        }

        public UniTask<string> GetTasks(string token, IProgress<float> progress = null)
        {
            return _serverless.GetTasks(token, progress);
        }

        public UniTask<JObject> GetTaskFolders(string token, IProgress<float> progress = null)
        {
            return _serverless.GetTaskFolders(token, progress);
        }

        public UniTask<string> GetAuthData(string email, string password, IProgress<float> progress = null)
        {
            return _serverless.GetAuthData(email, password, progress);
        }

        public UniTask RegisterUser(string login, string password, string name)
        {
            return _serverless.RegisterUser(login, password, name);
        }

        public UniTask RestorePassword(string login, string password)
        {
            return _serverless.RestorePassword(login, password);
        }
    }
}