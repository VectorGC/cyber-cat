using ApiGateway.Models;
using ApiGateway.Repositories;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using Shared.Services;

namespace ApiGateway.Services;

public static class AddGrpcServiceExtension
{
    public static IServiceCollection AddGrpcClientService(this IServiceCollection serviceCollection, string grpcHost)
    {
        serviceCollection.AddScoped<ITaskGrpcService>(provider =>
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:7001");
            var service = channel.CreateGrpcService<ITaskGrpcService>();
            return service;
        });

        return serviceCollection;
    }
}

public class SolutionService : ISolutionService
{
    private readonly ISolutionRepository _solutionRepository;

    public SolutionService(ISolutionRepository solutionRepository)
    {
        _solutionRepository = solutionRepository;
    }

    public async Task<string> GetLastSavedCode(UserId userId, string taskId)
    {
        return await _solutionRepository.GetSavedCode(userId, taskId);
    }

    public async Task SaveCode(UserId userId, string taskId, string code)
    {
        await _solutionRepository.SaveCode(userId, taskId, code);
    }
}