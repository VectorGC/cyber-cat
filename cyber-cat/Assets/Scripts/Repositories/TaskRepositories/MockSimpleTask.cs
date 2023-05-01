using Models;

namespace Repositories.TaskRepositories
{
    public class MockSimpleTask : ITask
    {
        public string Id => "Mock";

        public string Name => "It's the mock task";

        public string Description => "Это шаблон задания для разработки проекта";
    }
}