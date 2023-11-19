using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.ClientFactory;
using Shared.Server.Services;

namespace Shared.Server;

public static class ConfigurationExtension
{
    public static string GetMongoConnectionString(this IConfiguration configuration)
    {
        return configuration["MongoRepository:ConnectionString"];
    }

    public static string GetMongoDatabase(this IConfiguration configuration)
    {
        return configuration["MongoRepository:DatabaseName"];
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
}