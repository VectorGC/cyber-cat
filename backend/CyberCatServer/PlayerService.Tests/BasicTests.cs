using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PlayerService.GrpcServices;
using ProtoBuf.Grpc.Client;
using Shared.Models.Dto.Args;
using Shared.Server.Services;
using Shared.Tests;

namespace PlayerService.Tests;

[TestFixture]
public class BasicTests
{
    private WebApplicationFactory<Program> _factory;
    
    [SetUp]
    public void Setup()
    {
        _factory = new WebApplicationFactory<Program>();
    }

    /*[Test]
    public async Task CheckAndAddNewPlayer()
    {
        using var channel = _factory.CreateGrpcChannel();
        var service = channel.CreateGrpcService<IPlayerGrpcService>();

        var playerArgs = new PlayerArgs { UserId = 1234567 };

        var newPlayer = await service.GetPlayerById(playerArgs);

        Assert.IsNull(newPlayer);

        await service.AddNewPlayer(playerArgs);
        
        newPlayer = await service.GetPlayerById(playerArgs);
        
        Assert.IsNotNull(newPlayer);

        await service.DeletePlayer(playerArgs);
        
        newPlayer = await service.GetPlayerById(playerArgs);

        Assert.IsNull(newPlayer);
    }*/

    [Test]
    public async Task AddAndReturnNewPlayer()
    {
        using var channel = _factory.CreateGrpcChannel();
        var service = channel.CreateGrpcService<IPlayerGrpcService>();

        var playerArgs = new PlayerArgs { UserId = 1234567 };

        await service.AddNewPlayer(playerArgs);

        var newPlayer = await service.GetPlayerById(playerArgs);

        Assert.IsNotNull(newPlayer);

        await service.DeletePlayer(playerArgs);
    }
}