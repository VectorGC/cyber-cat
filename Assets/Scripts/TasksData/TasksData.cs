using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace TasksData
{
    [JsonObjectAttribute]
    public class TasksData : ITaskCollection
    {
        [JsonProperty("tasks")]
        public Dictionary<string, TaskData> Tasks { get; set; } = new Dictionary<string, TaskData>();

        #region | Delegate implementation |

        [UsedImplicitly]
        public Dictionary<string, TaskData>.Enumerator GetEnumerator()
        {
            return Tasks.GetEnumerator();
        }

        IEnumerator<ITaskTicket> IEnumerable<ITaskTicket>.GetEnumerator() => Tasks.Values.GetEnumerator();

        IEnumerator<KeyValuePair<string, ITaskTicket>> IEnumerable<KeyValuePair<string, ITaskTicket>>.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, ITaskTicket>>) Tasks).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) Tasks).GetEnumerator();

        public int Count => Tasks.Count;
        public bool ContainsKey(string key) => Tasks.ContainsKey(key);

        public bool TryGetValue(string key, out ITaskTicket value)
        {
            var success = Tasks.TryGetValue(key, out var taskData);
            value = taskData;

            return success;
        }

        public ITaskTicket this[string key] => Tasks[key];

        public IEnumerable<string> Keys => Tasks.Keys;

        public IEnumerable<ITaskTicket> Values => Tasks.Values;

        #endregion
    }
}