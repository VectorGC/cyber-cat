using System.Threading.Tasks;
using ApiGateway.Client.Services;
using Shared.Models.Dto;

namespace ApiGateway.Client.Internal.ServerlessServices
{
    internal class TaskRepositoryServerless : ITaskRepository
    {
        public Task<TaskDto> GetTask(string taskId)
        {
            var task = new TaskDto()
            {
                Name = "Hello world",
                Description = "Вы играете без сервера. Чтобы решать задачи - включите интернет"
            };

            return Task.FromResult(task);
        }
    }
}