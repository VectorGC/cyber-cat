using Shared.Models.Domain.Tasks;

namespace Shared.Models.Infrastructure
{
    public static class WebApi
    {
        public const string Login = "auth/login";
        public const string RegisterPlayer = "auth/registerPlayer";
        public const string RemoveUser = "auth/remove";

        public const string GetTaskDescriptions = "tasks";

        public static string GetTaskProgress(TaskId taskId) => $"player/tasks/{taskId.Value}";
        public const string GetTaskProgressTemplate = "player/tasks/{taskId}";
        public const string GetTasksProgress = "player/tasks";
        public static string SubmitSolution(TaskId taskId) => $"player/tasks/{taskId.Value}";
        public const string SubmitSolutionTemplate = "player/tasks/{taskId}";

        public const string GetTestCasesTemplate = "{taskId}/tests";
        public static string GetTestCases(TaskId taskId) => $"{taskId.Value}/tests";
    }
}