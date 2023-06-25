using System.Threading.Tasks;
using Shared.Models;
using Shared.Models.Models;

namespace ApiGateway.Client
{
    public interface ITaskRepository
    {
        Task<ITask> GetTask(string taskId);
    }

    public static class TaskRepositoryFactory
    {
        public static ITaskRepository Create(string uri, string token, IRestClient restClient)
        {
            var client = new Client(uri, restClient);
            client.AddAuthorizationToken(token);

            return client;
        }
    }
}