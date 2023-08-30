using System.Threading.Tasks;
using ApiGateway.Client.Services;
using Shared.Models.Dto;
using Shared.Models.Dto.Descriptions;

namespace ApiGateway.Client.Internal.ServerlessServices
{
    internal class TaskRepositoryServerless : ITaskRepository
    {
        public Task<TaskDto> GetTask(string taskId)
        {
            var task = new TaskDto()
            {
                Description = new TaskDescription()
                {
                    Name = "Hello world",
                    Description = "Вы играете без сервера. Чтобы решать задачи - включите интернет"
                }
            };

            return Task.FromResult(task);
        }
    }
}