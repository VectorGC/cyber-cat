using Models;
using Newtonsoft.Json;

namespace RestAPI.Dto
{
    public class TaskDto : ITask
    {
        [JsonProperty("name")] public string Name { get; set; } = string.Empty;
        [JsonProperty("desc")] public string Description { get; set; } = string.Empty;
    }
}