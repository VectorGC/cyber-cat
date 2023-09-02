using Microsoft.Extensions.Options;
using MongoDbGenericRepository;
using Shared.Models.Ids;
using Shared.Server.Dto;
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

        public async Task Add(TaskId taskId, TestsDto tests)
        {
            foreach (var test in tests)
            {
                await Add(taskId, test);
            }
        }

        private async Task Add(TaskId taskId, TestDto test)
        {
            var testModel = new TestDbModel(test);
            var task = await GetByIdAsync<TaskDbModel>(taskId);
            task.Tests.Add(testModel);

            var success = await UpdateOneAsync(task);
            if (!success)
            {
                throw new InvalidOperationException($"Failure update tests for task '{taskId}'");
            }
        }

        public async Task<TestsDto> GetTests(TaskId taskId)
        {
            var task = await GetByIdAsync<TaskDbModel>(taskId);
            return task.Tests.ToDto();
        }
    }
}