using System;
using Cysharp.Threading.Tasks;

namespace ServerAPIBase
{
    public interface IRestAPI<AuthT, RegisterT, RestorePasswordT, LastSavedCodeT, TasksT, TaskFoldersT, SendCodeT>
    {
        UniTask<SendCodeT> SendCodeToChecking(string token, string taskId, string code, string progLanguage,
            IProgress<float> progress = null);

        UniTask<TasksT> GetTasks(string token, IProgress<float> progress = null);
        UniTask<TaskFoldersT> GetTaskFolders(string token, IProgress<float> progress = null);

        UniTask<LastSavedCodeT> GetLastSavedCode(string token, string taskId, IProgress<float> progress = null);

        UniTask<AuthT> GetAuthData(string email, string password, IProgress<float> progress = null);
        UniTask<RegisterT> RegisterUser(string login, string password, string name);
        UniTask<RestorePasswordT> RestorePassword(string login, string password);
    }
}