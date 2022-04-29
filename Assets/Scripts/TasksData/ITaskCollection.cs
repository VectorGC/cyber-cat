using System.Collections.Generic;

namespace TasksData
{
    public interface ITaskCollection : IReadOnlyCollection<ITaskData>, IReadOnlyDictionary<string, ITaskData>
    {
    }
}