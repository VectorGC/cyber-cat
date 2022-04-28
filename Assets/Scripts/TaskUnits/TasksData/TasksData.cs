using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace TaskUnits.TasksData
{
    [JsonObject]
    internal class TasksData : ITaskDataCollection
    {
        [JsonProperty("tasks")]
        public Dictionary<string, TaskData> Tasks { get; set; } = new Dictionary<string, TaskData>();

        #region | Delegate implementation |

        [UsedImplicitly]
        public Dictionary<string, TaskData>.Enumerator GetEnumerator()
        {
            return Tasks.GetEnumerator();
        }

        IEnumerator<ITaskData> IEnumerable<ITaskData>.GetEnumerator() => Tasks.Values.GetEnumerator();

        IEnumerator<KeyValuePair<string, ITaskData>> IEnumerable<KeyValuePair<string, ITaskData>>.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, ITaskData>>) Tasks).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) Tasks).GetEnumerator();

        public int Count => Tasks.Count;
        public bool ContainsKey(string key) => Tasks.ContainsKey(key);

        public bool TryGetValue(string key, out ITaskData value)
        {
            var success = Tasks.TryGetValue(key, out var taskData);
            value = taskData;

            return success;
        }

        public ITaskData this[string key] => Tasks[key];

        public IEnumerable<string> Keys => Tasks.Keys;

        public IEnumerable<ITaskData> Values => Tasks.Values;

        #endregion
    }
}