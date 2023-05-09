using ServerAPIBase;
using System;
using Cysharp.Threading.Tasks;
using TaskUnits;

namespace RestAPIWrapper
{
    public abstract class WebApiProxy<AggregatorT, AuthentificateT, RegisterT, RestorePasswordT, GetTokenT, SendCodeT, ReceiveCodeT, GetTasksT>
    {
        public IWebApiAggregator<AggregatorT> WebApiAggregator { get; protected set; }

        public abstract void Authentificate(string login, string password, string email, string token, Action<AuthentificateT> callback = null);
        public abstract UniTask<AuthentificateT> Authentificate(string login, string password, string email, string token);

        public abstract void Register(string login, string password, Action<RegisterT> callback = null);
        public abstract UniTask<RegisterT> Register(string login, string password);

        public abstract void RestorePassword(string login, string password, Action<RestorePasswordT> callback = null);
        public abstract UniTask<RestorePasswordT> RestorePassword(string login, string password);

        public abstract void GetToken(string login, string password, string email, Action<GetTokenT> callback = null, bool local = false);
        public abstract UniTask<GetTokenT> GetToken(string login, string password, string email, bool local = false);

        public abstract void SendCode(ITaskData taskData, string token, string code, string progLanguage, Action<SendCodeT> callback = null);
        public abstract UniTask<SendCodeT> SendCode(ITaskData taskData, string token, string code, string progLanguage);

        public abstract void ReceiveCode(string token, string taskID, Action<ReceiveCodeT> callback = null);
        public abstract UniTask<ReceiveCodeT> ReceiveCode(string token, string taskID);

        public abstract void GetTasks(string token, Action<GetTasksT> callback = null);
        public abstract UniTask<GetTasksT> GetTasks(string token);
    }
}