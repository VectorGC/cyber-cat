using System.Collections.Generic;
using Models;
using Newtonsoft.Json;

namespace ServerAPI.InternalDto
{
    [JsonObject]
    internal class TasksDto
    {
        [JsonProperty("tasks")] public Dictionary<string, TaskDto> Tasks { get; set; } = new Dictionary<string, TaskDto>();

        public ITask this[string taskId] => Tasks[taskId];
    }
}