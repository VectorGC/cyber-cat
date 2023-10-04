using Microsoft.Extensions.Options;
using MongoDbGenericRepository;
using Shared.Models.Ids;
using Shared.Models.Models.TestCases;
using TaskService.Configurations;
using TaskService.Repositories.InternalModels;

namespace TaskService.Repositories
{
    public class TestMongoRepository : BaseMongoRepository<string>, ITestRepository
    {
        public TestMongoRepository(IOptions<TaskServiceAppSettings> appSettings)
            : base(appSettings.Value.MongoRepository.ConnectionString, appSettings.Value.MongoRepository.DatabaseName)
        {
        }

        public async Task Add(TestCases testCases)
        {
            foreach (var (id, testCase) in testCases.Values)
            {
                await Add(id, testCase);
            }
        }

        private async Task Add(TestCaseId id, TestCase testCase)
        {
            var testModel = new TestDbModel(testCase);
            var task = await GetByIdAsync<TaskDbModel>(id.TaskId);
            if (task.Tests.Count > id.Index)
            {
                task.Tests.RemoveAt(id.Index);
            }

            task.Tests.Insert(id.Index, testModel);

            var success = await UpdateOneAsync(task);
            if (!success)
            {
                throw new InvalidOperationException($"Failure update test case '{id}'");
            }
        }

        public async Task<TestCases> GetTestCases(TaskId taskId)
        {
            var task = await GetByIdAsync<TaskDbModel>(taskId);
            return task.Tests.ToDescription(taskId);
        }
    }
}