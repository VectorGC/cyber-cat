using Models;
using Newtonsoft.Json;

namespace Repositories.TaskRepositories
{
    public class TaskModel : ITask
    {
        [JsonProperty("name")] public string Name { get; set; } = string.Empty;
        [JsonProperty("desc")] public string Description { get; set; } = string.Empty;
    }
}