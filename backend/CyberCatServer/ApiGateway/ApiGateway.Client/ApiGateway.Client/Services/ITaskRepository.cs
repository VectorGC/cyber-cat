using System.Threading.Tasks;
using Shared.Models.Dto;

namespace ApiGateway.Client.Services
{
    public interface ITaskRepository
    {
        Task<TaskDto> GetTask(string taskId);
    }
}