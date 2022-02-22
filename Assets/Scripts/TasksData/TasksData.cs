using System.Collections.Generic;
using Newtonsoft.Json;

namespace TasksData
{
    public class TasksData
    {
        [JsonProperty("tasks")] public Dictionary<string, TaskData> Tasks { get; set; }
    }
}