using System.Collections.Generic;

namespace TasksData
{
    public interface ITaskCollection : IReadOnlyCollection<ITaskTicket>, IReadOnlyDictionary<string, ITaskTicket>
    {
    }
}