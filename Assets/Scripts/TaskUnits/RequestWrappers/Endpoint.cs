using Endpoint;

namespace TasksData.RequestWrappers
{
    internal static class Endpoint
    {
        internal const string TASKS_HIERARCHY = MainEndpoint.Uri + "/tasks/hierarchy";
        internal const string TASKS_FLAT = MainEndpoint.Uri + "/tasks/flat";
    }
}