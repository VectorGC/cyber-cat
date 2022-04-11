using Newtonsoft.Json;

namespace TasksData
{
    public class TaskData : ITaskData
    {
        [JsonProperty("id")] public string Id { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("desc")] public string Description { get; set; }
        [JsonProperty("is_passed")] public bool IsSolved { get; set; }
        [JsonProperty("output")] public string Output { get; set; }
    }
}