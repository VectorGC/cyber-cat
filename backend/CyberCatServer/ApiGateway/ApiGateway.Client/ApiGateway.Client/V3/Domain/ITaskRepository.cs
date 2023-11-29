using System.Collections.Generic;
using Shared.Models.Ids;

namespace ApiGateway.Client.V3.Domain
{
    public interface ITaskRepository : IReadOnlyDictionary<TaskId, TaskModel>
    {
        
    }
}