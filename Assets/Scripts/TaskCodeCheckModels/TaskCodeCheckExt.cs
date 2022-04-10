using CodeEditorModels.ProgLanguages;
using Cysharp.Threading.Tasks;
using TasksData;

namespace TaskCodeCheckModels
{
    public static class TaskCodeCheckExt
    {
        public static async UniTask<CodeCheckingResult> CheckCodeAsync(this ITaskData taskTicket, string code, ProgLanguage progLanguage)
        {
            return await TaskCodeCheck.CheckCodeAsync(taskTicket.Id, code, progLanguage);
        }
    }
}