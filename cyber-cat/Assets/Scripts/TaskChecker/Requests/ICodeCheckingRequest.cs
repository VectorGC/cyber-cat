using System;
using Cysharp.Threading.Tasks;
using ServerAPIBase;

namespace TaskChecker.Requests
{
    internal interface ICodeCheckingRequest
    {
        public UniTask<ICodeConsoleMessage> SendCodeToChecking(string token, string taskId, string code,
            string progLanguage, IProgress<float> progress = null);
    }
}