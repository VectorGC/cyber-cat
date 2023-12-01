using System.Collections.Generic;
using Shared.Models.Domain.Tasks;

namespace ApiGateway.Client.Models
{
    public interface ITaskRepository : IReadOnlyDictionary<TaskId, ITask>
    {
    }
}