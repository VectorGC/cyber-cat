using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using MongoDbGenericRepository;
using Shared.Models.Models;
using Shared.Server.Exceptions;
using SolutionService.Repositories.InternalModels;

namespace SolutionService.Repositories;

public class SolutionMongoRepository : BaseMongoRepository, ISolutionRepository
{
    public SolutionMongoRepository(IOptions<SolutionServiceAppSettings> appSettings)
        : base(appSettings.Value.MongoRepository.ConnectionString, appSettings.Value.MongoRepository.DatabaseName)
    {
        var indexes = new List<Expression<Func<SolutionModel, object>>>
        {
            s => s.UserId,
            s => s.TaskId
        };

        CreateCombinedTextIndexAsync(indexes);
    }

    public async Task<string> GetSavedCode(string userId, string taskId)
    {
        var solution = await GetOneAsync<SolutionModel>(s => s.UserId == userId && s.TaskId == taskId);
        return solution?.SourceCode;
    }

    public async Task Save(string userId, ISolution solution)
    {
        var solutionModel = await GetOneAsync<SolutionModel>(s => s.UserId == userId && s.TaskId == solution.TaskId);
        if (solutionModel == null)
        {
            solutionModel = new SolutionModel(userId, solution);
            await AddOneAsync(solutionModel);
            return;
        }

        solutionModel.SourceCode = solution.SourceCode;
        var success = await UpdateOneAsync(solutionModel);
        if (!success)
        {
            throw new SaveCodeException($"Failure save code solution for user '{userId}' by task '{solution.TaskId}'");
        }
    }

    public async Task RemoveCode(string userId, string taskId)
    {
        await DeleteOneAsync<SolutionModel>(s => s.UserId == userId && s.TaskId == taskId);
    }
}