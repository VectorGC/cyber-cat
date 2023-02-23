using Authentication;
using Cysharp.Threading.Tasks;
using GameCodeEditor.Scripts;
using System;
using TaskChecker;
using TaskUnits;

namespace RequestAPI.Proxy
{
    public static class RequestAPIProxy
    {
        public static async UniTask Register(string login, string password, string name)
        {
            await TokenSession.RegisterUser(login, password, name);
        }

        public static async UniTask RestorePassword(string login, string password)
        {
            await TokenSession.RestorePassword(login, password);
        }

        public static async UniTask<TokenSession> Authenticate(string login, string password)
        {
            return await TokenSession.RequestAndSaveFromServer(login, password);
        }

        public static async UniTask<ICodeConsoleMessage> CheckCode(ITaskData task, string token, string code, string progLanguage)
        {
            return await task.CheckCodeAsync(token, code, progLanguage);
        }

        public static async UniTask<string> GetSavedCode(string token, string taskId)
        {
            return await new GetLastSaveCodeRequest().GetLastSavedCode(token, taskId);
        }

        public static async UniTask<TokenSession> GetTokenFromServer(string login, string password)
        {
            return await TokenSession.RequestFromServer(login, password);
        }

        public static async UniTask<ITaskDataCollection> GetTasks(string token, IProgress<float> progress = null)
        {
            return await TaskFacade.GetAllTasks(token, progress);
        }
    }
}


