using Microsoft.Extensions.Options;
using MongoDbGenericRepository;
using Shared.Models.Models;
using TaskService.Configurations;
using TaskService.Repositories.InternalModels;

namespace TaskService.Repositories;

public class TestMongoRepository : BaseMongoRepository<string>, ITestRepository
{
    public TestMongoRepository(IOptions<TaskServiceAppSettings> appSettings)
        : base(appSettings.Value.MongoRepository.ConnectionString, appSettings.Value.MongoRepository.DatabaseName)
    {
    }

    public async Task Add(string taskId, IEnumerable<ITest> tests)
    {
        foreach (var test in tests)
        {
            await Add(taskId, test);
        }
    }

    public async Task Add(string taskId, ITest test)
    {
        var testModel = new TestModel(test);
        var task = await GetOneAsync<TaskModel>(task => task.Id == taskId);
        task.Tests.Add(testModel);
        
        

        var success = await UpdateOneAsync(task);
        if (!success)
        {
            throw new InvalidOperationException($"Failure update tests for task '{taskId}'");
        }
    }

    public async Task<ITests> GetTests(string taskId)
    {
        var task = await GetOneAsync<TaskModel>(task => task.Id == taskId);
        return task.GetTests();
    }
}