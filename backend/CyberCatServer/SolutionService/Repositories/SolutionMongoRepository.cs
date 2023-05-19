using System.Linq.Expressions;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDbGenericRepository;
using Shared.Exceptions;
using Shared.Models;
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

    public async Task<string?> GetSavedCode(string userId, string taskId)
    {
        var solution = await GetOneAsync<SolutionModel>(s => s.UserId == userId && s.TaskId == taskId);
        return solution?.SourceCode;
    }

    public async Task Save(ISolution solution)
    {
        var solutionModel = await GetOneAsync<SolutionModel>(s => s.UserId == solution.UserId && s.TaskId == solution.TaskId);
        if (solutionModel == null)
        {
            solutionModel = solution.To<SolutionModel>();
            await AddOneAsync(solutionModel);
            return;
        }

        var id = solutionModel.Id;
        solutionModel = solution.To<SolutionModel>();
        solutionModel.Id = id;

        var success = await UpdateOneAsync(solutionModel);
        if (!success)
        {
            throw new SaveCodeException($"Failure save code solution for user '{solution.UserId}' by task '{solution.TaskId}'");
        }
    }

    public async Task RemoveCode(string userId, string taskId)
    {
        await DeleteOneAsync<SolutionModel>(s => s.UserId == userId && s.TaskId == taskId);
    }
}