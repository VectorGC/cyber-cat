using Microsoft.Extensions.Options;
using MongoDbGenericRepository;
using Shared.Exceptions;
using SolutionService.Repositories.InternalModels;

namespace SolutionService.Repositories;

public class SolutionMongoRepository : BaseMongoRepository, ISolutionRepository
{
    public SolutionMongoRepository(IOptions<SolutionServiceAppSettings> appSettings)
        : base(appSettings.Value.MongoRepository.ConnectionString, appSettings.Value.MongoRepository.DatabaseName)
    {
    }

    public async Task<string?> GetSavedCode(string userId, string taskId)
    {
        var solution = await GetOneAsync<SolutionModel>(s => s.UserId == userId && s.TaskId == taskId);
        return solution?.SourceCode;
    }

    public async Task SaveCode(string userId, string taskId, string sourceCode)
    {
        var solution = await GetOneAsync<SolutionModel>(s => s.UserId == userId && s.TaskId == taskId);
        if (solution == null)
        {
            solution = new SolutionModel
            {
                UserId = userId,
                TaskId = taskId,
                SourceCode = sourceCode
            };

            await AddOneAsync(solution);
            return;
        }

        solution.SourceCode = sourceCode;

        var success = await UpdateOneAsync(solution);
        if (!success)
        {
            throw new SaveCodeException($"Failure save code solution for user '{userId}' by task '{taskId}'");
        }
    }

    public async Task RemoveCode(string userId, string taskId)
    {
        await DeleteOneAsync<SolutionModel>(s => s.UserId == userId && s.TaskId == taskId);
    }
}