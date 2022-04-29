using System;
using Authentication;
using CodeEditorModels.ProgLanguages;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using RestAPIWrapper;

namespace TaskCodeCheckModels
{
    public static class TaskCodeCheck
    {
        public static async UniTask<CodeCheckingResult> CheckCodeAsync(string taskId, string code, ProgLanguage progLanguage)
        {
            try
            {
                var checkingResult = await CheckCodeStringAsync(taskId, code, progLanguage);
                return JsonConvert.DeserializeObject<CodeCheckingResult>(checkingResult);
            }
            catch (Exception ex)
            {
                CodeConsole.WriteLine(ex);
                throw;
            }
        }

        private static async UniTask<string> CheckCodeStringAsync(string taskId, string code, ProgLanguage progLanguage)
        {
            var token = TokenSession.FromPlayerPrefs();
            return await RestAPI.SendCodeToChecking(token, taskId, code, progLanguage); 
        }
    }
}