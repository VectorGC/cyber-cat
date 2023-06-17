using System.Threading.Tasks;
using Shared.Models;

namespace ApiGateway.Client
{
    public interface ITaskRepository
    {
        Task<ITask> GetTask(string taskId);
    }

    public static class TaskRepositoryFactory
    {
        public static ITaskRepository Create(string uri, string token)
        {
            var client = new Client(uri);
            client.AddAuthorizationToken(token);

            return client;
        }
    }
}