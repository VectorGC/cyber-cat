using System.Threading.Tasks;
using NUnit.Framework;
using Repositories.TaskRepositories;
using ServerAPI;

namespace Tests
{
    [TestFixture]
    public class TaskRepositoryTests
    {
        private readonly ITaskRepository _taskRepository = new TaskRepositoryProxy(RestAPIFacade.Create());

        [Test]
        public async Task HasTutorialTask_InRepository([Values("tutorial")] string taskId)
        {
            var task = await _taskRepository.GetTask(taskId);

            Assert.AreEqual("Hello world!", task.Name);
            Assert.IsNotEmpty(task.Description);
        }
    }
}