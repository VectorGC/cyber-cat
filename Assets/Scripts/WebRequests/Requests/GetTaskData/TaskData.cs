using Newtonsoft.Json;

namespace ServerData
{
    public class TaskData
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("desc")] public string Description { get; set; }
    }
}