using System;
using Cysharp.Threading.Tasks;
using RestAPIWrapper;

namespace TaskChecker.Requests
{
    internal struct CodeCheckingRequest : ICodeCheckingRequest
    {
        public async UniTask<ICodeConsoleMessage> SendCodeToChecking(string token, string taskId, string code,
            string progLanguage, IProgress<float> progress = null)
            => await RestAPI.Instance.SendCodeToChecking(token, taskId, code, progLanguage, progress);
    }
}