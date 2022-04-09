using CodeEditorModels.ProgLanguages;
using Cysharp.Threading.Tasks;

namespace TaskCodeCheckModels
{
    public static class TaskCodeCheckExt
    {
        public static async UniTask<CodeCheckingResult> CheckCodeAsync(this ITaskTicket taskTicket, string code, ProgLanguage progLanguage)
        {
            return await TaskCodeCheck.CheckCodeAsync(taskTicket.Id, code, progLanguage);
        }
    }
}