using System.Threading.Tasks;
using NUnit.Framework;
using Repositories.TaskRepositories;
using ServerAPI;

namespace Tests
{
    [TestFixture]
    public class TaskRepositoryTests
    {
        private readonly ITaskRepository2 _taskRepository2 = new TaskRepository2Proxy(ServerAPIFacade.Create());
    }
}