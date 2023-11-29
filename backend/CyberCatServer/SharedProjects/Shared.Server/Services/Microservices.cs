using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDbGenericRepository;
using ProtoBuf.Grpc.ClientFactory;

namespace Shared.Server.Services;

public static class Microservices
{
    public static IServiceCollection AddMongoDatabaseContext(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddScoped(provider => provider.GetRequiredService<IConfiguration>().GetMongoDatabaseContext());
    }

    public static IMongoDbContext GetMongoDatabaseContext(this IConfiguration configuration)
    {
        var connectionString = configuration["MongoRepository:ConnectionString"];
        var databaseName = configuration["MongoRepository:DatabaseName"];
        var database = new MongoClient(connectionString).GetDatabase(databaseName);

        return new MongoDbContext(database);
    }

    public static IHttpClientBuilder AddAuthServiceGrpcClient(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("AuthServiceGrpcEndpoint");
        return builder.Services.AddCodeFirstGrpcClient<IAuthService>(options => { options.Address = new Uri(connectionString); });
    }

    public static IHttpClientBuilder AddTaskServiceGrpcClient(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("TaskServiceGrpcEndpoint");
        return builder.Services.AddCodeFirstGrpcClient<ITaskService>(options => { options.Address = new Uri(connectionString); });
    }

    public static IHttpClientBuilder AddJudgeServiceGrpcClient(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("JudgeServiceGrpcEndpoint");
        return builder.Services.AddCodeFirstGrpcClient<IJudgeService>(options => { options.Address = new Uri(connectionString); });
    }

    public static IHttpClientBuilder AddPlayerServiceGrpcClient(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("PlayerServiceGrpcEndpoint");
        return builder.Services.AddCodeFirstGrpcClient<IPlayerService>(options => { options.Address = new Uri(connectionString); });
    }
}