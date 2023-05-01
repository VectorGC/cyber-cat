using System.Collections.Generic;
using Newtonsoft.Json;

namespace Repositories.TaskRepositories
{
    [JsonObject]
    public class TaskModels
    {
        [JsonProperty("tasks")] public Dictionary<string, TaskModel> Tasks { get; set; } = new Dictionary<string, TaskModel>();
    }
}