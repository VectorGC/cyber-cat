using System.Collections.Generic;
using Newtonsoft.Json;

namespace ServerData
{
    public class AvailableTasksData
    {
        [JsonProperty("tasks")] public Dictionary<string, TaskData> Tasks { get; set; }
    }
}