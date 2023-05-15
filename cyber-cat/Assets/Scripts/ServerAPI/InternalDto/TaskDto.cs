using Models;
using Newtonsoft.Json;

namespace ServerAPI.InternalDto
{
    internal class TaskDto : ITask
    {
        [JsonProperty("name")] public string Name { get; set; } = string.Empty;
        [JsonProperty("description")] public string Description { get; set; } = string.Empty;
    }
}