using System.Collections.Generic;
using Shared.Models.Ids;

namespace ApiGateway.Client.Models
{
    public interface ITaskRepository : IReadOnlyDictionary<TaskId, ITask>
    {
    }
}