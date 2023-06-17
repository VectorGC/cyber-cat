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

        [Test]
        public async Task HasTutorialTask_InRepository([Values("tutorial")] string taskId)
        {
            var task = await _taskRepository2.GetTask(taskId);

            Assert.AreEqual("Hello world!", task.Name);
            Assert.IsNotEmpty(task.Description);
        }
    }
}