using ApiGateway.Models;
using ApiGateway.Repositories.Models;
using MongoDB.Driver;

namespace ApiGateway.Repositories;

public interface ISolutionRepository
{
    Task<string> GetSavedCode(UserId userId, string taskId);
    Task SaveCode(UserId userId, string taskId, string code);
}

public class SolutionRepository : ISolutionRepository
{
    private readonly IMongoCollection<SolutionCodeDbModel> _solutionCollection;

    public SolutionRepository(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDatabase");
        var client = new MongoClient(connectionString);
        var cyberCatDb = client.GetDatabase("CyberCat");
        _solutionCollection = cyberCatDb.GetCollection<SolutionCodeDbModel>("Solutions");
    }

    public async Task<string> GetSavedCode(UserId userId, string taskId)
    {
        var code = await _solutionCollection
            .Find(s => s.AuthorEmail == userId && s.TaskId == taskId)
            .Project(s => s.SourceCode)
            .FirstOrDefaultAsync(GetCT());

        return code ?? string.Empty;
    }

    public async Task SaveCode(UserId userId, string taskId, string code)
    {
        var update = Builders<SolutionCodeDbModel>.Update.Set(s => s.SourceCode, code);
        var options = new FindOneAndUpdateOptions<SolutionCodeDbModel>
        {
            IsUpsert = true // Если записи в БД нет - upsert её автоматически создаст.
        };

        await _solutionCollection.FindOneAndUpdateAsync<SolutionCodeDbModel>(
            s => s.AuthorEmail == userId && s.TaskId == taskId,
            update, options);
    }

    private CancellationToken GetCT()
    {
        // Ни одни запрос не должен превышать 5 секунд. Если превышает - вызывается отмена таски и эксепшн.
        return new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token;
    }
}