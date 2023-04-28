using Models;
using Newtonsoft.Json;

namespace Repositories.TaskRepositories
{
    public class TaskModel : ITask
    {
        [JsonProperty("id")] public string Id { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("desc")] public string Description { get; set; }
    }
}