using System.Threading.Tasks;
using Shared.Models;
using Shared.Models.Models;

namespace ApiGateway.Client
{
    public interface ITaskRepository
    {
        Task<ITask> GetTask(string taskId);
    }
}