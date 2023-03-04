using MongoDB.Bson;
using MongoDB.Driver;

namespace ApiGateway.BackgroundServices;

public class MongoDbConnectionCheck : BackgroundService
{
    private readonly MongoClient _client;

    public MongoDbConnectionCheck(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        _client = new MongoClient(connectionString);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (true)
        {
            var cyberCatDb = _client.GetDatabase("CyberCat");

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));

            var result = await cyberCatDb.RunCommandAsync((Command<BsonDocument>) "{ping:1}", cancellationToken: cts.Token);
            Console.WriteLine(result);

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}