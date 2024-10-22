using Shared.Models.Domain.Tasks;

namespace Shared.Models.Infrastructure
{
    public static class WebApi
    {
        // Allow Anonymous
        public const string Login = "auth/login";
        public const string RegisterPlayer = "auth/registerPlayer";
        public const string LoginWithVk = "auth/vk/login";

        public const string GetTaskDescriptions = "tasks";
        public const string GetTestCasesTemplate = "tasks/{taskId}/tests";
        public static string GetTestCases(TaskId taskId) => $"tasks/{taskId.Value}/tests";

        public const string GetUsersWhoSolvedTaskTemplate = "player/tasks/{taskId}/usersWhoSolvedTask";
        public static string GetUsersWhoSolvedTask(TaskId taskId) => $"player/tasks/{taskId}/usersWhoSolvedTask";

        public const string JudgeGetVerdict = "judge/submit";

        // Only Player
        public const string RemoveUser = "auth/remove";
        public static string GetTaskProgress(TaskId taskId) => $"player/tasks/{taskId.Value}";
        public const string GetTaskProgressTemplate = "player/tasks/{taskId}";
        public const string GetTasksProgress = "player/tasks";
        public static string SubmitSolution(TaskId taskId) => $"player/tasks/{taskId.Value}";
        public const string SubmitSolutionTemplate = "player/tasks/{taskId}";
        public const string SaveVerdictHistory = "player/verdicts/save";
    }
}