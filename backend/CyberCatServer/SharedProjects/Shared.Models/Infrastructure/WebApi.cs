using Shared.Models.Ids;

namespace Shared.Models.Infrastructure
{
    public static class WebApi
    {
        public const string Login = "auth/login";
        public const string Register = "auth/register";
        public const string Remove = "auth/remove";
        public const string GetCurrentUserData = "auth/user";

        public const string GetTaskDescriptions = "tasks";
        public const string GetTaskProgressDataTemplate = "tasks/{taskId}";
        public static string GetTaskProgressData(TaskId taskId) => $"player/tasks/{taskId.Value}";
        public const string GetTasksProgressData = "player/tasks";
        public static string SubmitSolution(TaskId taskId) => $"player/tasks/{taskId.Value}";
    }
}