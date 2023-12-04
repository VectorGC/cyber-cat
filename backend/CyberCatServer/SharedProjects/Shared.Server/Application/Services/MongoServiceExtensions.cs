using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDbGenericRepository;

namespace Shared.Server.Application.Services;

public static class MongoServiceExtensions
{
    public static IServiceCollection AddMongoDatabaseContext(this WebApplicationBuilder builder)
    {
        return builder.Services.AddScoped(provider => provider.GetRequiredService<IConfiguration>().GetMongoDatabaseContext());
    }

    public static IMongoDbContext GetMongoDatabaseContext(this IConfiguration configuration)
    {
        var connectionString = configuration["MongoRepository:ConnectionString"];
        var databaseName = configuration["MongoRepository:DatabaseName"];
        var database = new MongoClient(connectionString).GetDatabase(databaseName);

        return new MongoDbContext(database);
    }
}