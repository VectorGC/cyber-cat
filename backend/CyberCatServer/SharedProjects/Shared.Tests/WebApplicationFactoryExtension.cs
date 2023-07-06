using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Tests;

public static class WebApplicationFactoryExtension
{
    public static GrpcChannel CreateGrpcChannel<TProgram>(this WebApplicationFactory<TProgram> factory) where TProgram : class
    {
        var channel = GrpcChannel.ForAddress(factory.Server.BaseAddress, new GrpcChannelOptions
        {
            HttpHandler = factory.Server.CreateHandler()
        });

        return channel;
    }

    public static WebApplicationFactory<TProgram> AddScoped<TProgram, TInterface, TService>(this WebApplicationFactory<TProgram> factory)
        where TProgram : class where TInterface : class where TService : class, TInterface
    {
        return factory.WithWebHostBuilder(builder => { builder.ConfigureTestServices(services => { services.AddScoped<TInterface, TService>(); }); });
    }

    public static WebApplicationFactory<TProgram> AddScoped<TProgram, TInterface>(this WebApplicationFactory<TProgram> factory, TInterface implementation)
        where TProgram : class where TInterface : class
    {
        return factory.WithWebHostBuilder(builder => { builder.ConfigureTestServices(services => { services.AddScoped(_ => implementation); }); });
    }
}