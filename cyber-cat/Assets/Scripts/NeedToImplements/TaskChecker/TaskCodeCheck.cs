using System;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using TaskChecker.Requests;
using TaskChecker.Verdicts;

namespace TaskChecker
{
    public static class TaskCodeCheck
    {
        public static async UniTask<ICodeConsoleMessage> CheckCodeAsync(string token, string taskId, string code,
            string progLanguage)
        {
            try
            {
                var checkingResult = await CheckCodeStringAsync(token, taskId, code, progLanguage);
                return JsonConvert.DeserializeObject<VerdictResult>(checkingResult);
            }
            catch (Exception ex)
            {
                CodeConsoleOld.WriteLine(ex);
                throw;
            }
        }

        private static async UniTask<string> CheckCodeStringAsync(string token, string taskId, string code,
            string progLanguage)
        {
            return (await new CodeCheckingRequest().SendCodeToChecking(token, taskId, code, progLanguage)).Message;
        }
    }
}