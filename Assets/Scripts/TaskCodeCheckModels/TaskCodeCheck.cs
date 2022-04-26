using System;
using System.Collections.Generic;
using Authentication;
using CodeEditorModels.ProgLanguages;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using RestAPIWrapper;

namespace TaskCodeCheckModels
{
    public static class TaskCodeCheck
    {
        private static readonly Dictionary<ProgLanguage, string> ProgLanguages = new Dictionary<ProgLanguage, string>()
        {
            [ProgLanguage.Cpp] = "cpp",
            [ProgLanguage.Python] = "py",
            [ProgLanguage.Pascal] = "pas"
        };

        public static async UniTask<CodeCheckingResult> CheckCodeAsync(string taskId, string code,
            ProgLanguage progLanguage)
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

            var progLanguageString = ProgLanguages[progLanguage];
            return await new CodeCheckingRequest().SendCodeToChecking(token, taskId, code, progLanguageString);
        }
    }
}