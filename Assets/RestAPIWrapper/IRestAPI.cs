using System;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace RestAPIWrapper
{
    public interface IRestAPI
    {
        UniTask<string> SendCodeToChecking(string token, string taskId, string code, string progLanguage,
            IProgress<float> progress = null);

        UniTask<string> GetTasks(string token, IProgress<float> progress = null);
        UniTask<JObject> GetTaskFolders(string token, IProgress<float> progress = null);

        UniTask<string> GetLastSavedCode(string token, string taskId, IProgress<float> progress = null);
        
        UniTask<string> GetAuthData(string login, string password, IProgress<float> progress = null);
        UniTask RegisterUser(string login, string password, string name);
        UniTask RestorePassword(string login, string password);
    }
}