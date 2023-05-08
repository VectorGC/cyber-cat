using System.Collections.Generic;

namespace TaskUnits
{
    public interface ITaskDataCollection : IReadOnlyCollection<ITaskData>, IReadOnlyDictionary<string, ITaskData>
    {
    }
}