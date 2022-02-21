using System.Collections.Generic;
using Newtonsoft.Json;
using WebRequests.Requests.GetTasksData;

namespace ServerData
{
    public class TasksData
    {
        [JsonProperty("tasks")] public Dictionary<string, TaskData> Tasks { get; set; }
    }
}