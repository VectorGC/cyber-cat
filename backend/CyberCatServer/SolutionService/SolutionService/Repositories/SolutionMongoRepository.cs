using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using MongoDbGenericRepository;
using Shared.Models.Dto;
using Shared.Models.Models;
using Shared.Server.Exceptions;
using Shared.Server.Models;
using SolutionService.Configurations;
using SolutionService.Repositories.InternalModels;

namespace SolutionService.Repositories;

public class SolutionMongoRepository : BaseMongoRepository, ISolutionRepository
{
    public SolutionMongoRepository(IOptions<SolutionServiceAppSettings> appSettings)
        : base(appSettings.Value.MongoRepository.ConnectionString, appSettings.Value.MongoRepository.DatabaseName)
    {
        var indexes = new List<Expression<Func<SolutionDbModel, object>>>
        {
            s => s.UserId,
            s => s.TaskId
        };

        CreateCombinedTextIndexAsync(indexes);
    }

    public async Task<string> GetSavedCode(UserId userId, TaskId taskId)
    {
        var solution = await GetOneAsync<SolutionDbModel>(s => s.UserId == userId.Value && s.TaskId == taskId.Value);
        return solution?.SourceCode;
    }

    public async Task Save(UserId userId, SolutionDto solution)
    {
        var solutionModel = await GetOneAsync<SolutionDbModel>(s => s.UserId == userId.Value && s.TaskId == solution.TaskId.Value);
        if (solutionModel == null)
        {
            solutionModel = new SolutionDbModel(userId, solution);
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

    public async Task RemoveCode(UserId userId, TaskId taskId)
    {
        await DeleteOneAsync<SolutionDbModel>(s => s.UserId == userId.Value && s.TaskId == taskId.Value);
    }
}