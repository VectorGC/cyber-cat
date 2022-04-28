using System;
using Cysharp.Threading.Tasks;

namespace TaskChecker.Requests
{
    internal interface ICodeCheckingRequest
    {
        public UniTask<string> SendCodeToChecking(string token, string taskId, string code,
            string progLanguage, IProgress<float> progress = null);
    }
}