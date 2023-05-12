using System.Collections.Generic;
using Models;
using Newtonsoft.Json;

namespace RestAPI.InternalDto
{
    [JsonObject]
    internal class TasksDto : ITasks
    {
        [JsonProperty("tasks")] public Dictionary<string, TaskDto> Tasks { get; set; } = new Dictionary<string, TaskDto>();

        public ITask this[string taskId] => Tasks[taskId];
    }
}