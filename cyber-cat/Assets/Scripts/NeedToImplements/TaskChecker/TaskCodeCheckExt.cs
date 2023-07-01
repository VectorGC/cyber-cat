using Cysharp.Threading.Tasks;
using TaskUnits;

namespace TaskChecker
{
    public static class TaskCodeCheckExt
    {
        public static async UniTask<ICodeConsoleMessage> CheckCodeAsync(this ITaskData taskTicket, string token,
            string code, string progLanguage)
        {
            return await TaskCodeCheck.CheckCodeAsync(token, taskTicket.Id, code, progLanguage);
        }
    }
}