using System;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace RestAPIWrapper.Serverless
{
    public class RestAPIServerless : IRestAPI
    {
        public UniTask<string> SendCodeToChecking(string token, string taskId, string code, string progLanguage,
            IProgress<float> progress = null)
        {
            return UniTask.FromResult("{\"error\":\"Это режим 'без сервера', чтобы тесировать задачи - подключите сервер и уберите директиву SERVERLESS при сборке проекта\"}");
        }

        public UniTask<string> GetTasks(string token, IProgress<float> progress = null)
        {
            throw new NotImplementedException();
        }

        public UniTask<JObject> GetTaskFolders(string token, IProgress<float> progress = null)
        {
            throw new NotImplementedException();
        }
    }
}