using Newtonsoft.Json;

namespace TasksData
{
    public class TaskData : ITaskTicket
    {
        [JsonProperty("unit_id")] public int Id { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("desc")] public string Description { get; set; }
        [JsonProperty("is_passed")] public bool IsPassed { get; set; }
    }
}