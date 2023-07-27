using Microsoft.AspNetCore.Mvc.Testing;
using ProtoBuf.Grpc.Client;
using Shared.Models.Dto.Args;
using Shared.Server.Services;
using Shared.Tests;

namespace PlayerService.Tests;

[TestFixture]
public class BitcoinsTests
{
    private WebApplicationFactory<Program> _factory;
    
    [SetUp]
    public void Setup()
    {
        _factory = new WebApplicationFactory<Program>();
    }
    
    [Test]
    public async Task CreatePlayerAndAddSomeBtc()
    {
        using var channel = _factory.CreateGrpcChannel();
        var service = channel.CreateGrpcService<IPlayerGrpcService>();
        
        var createPlayerArgs = new PlayerIdArgs { PlayerId = 1234567 };

        await service.AddNewPlayer(createPlayerArgs);
        
        var newPlayer = await service.GetPlayerById(createPlayerArgs);
        
        Assert.IsNotNull(newPlayer);
        Assert.That(newPlayer.UserId, Is.EqualTo(1234567));

        var addBtcPlayerArgs = new PlayerBtcArgs { PlayerId = createPlayerArgs.PlayerId, BitcoinsCount = 1000 };

        await service.AddBitcoinsToPlayer(addBtcPlayerArgs);
        
        newPlayer = await service.GetPlayerById(createPlayerArgs);
        Assert.IsNotNull(newPlayer);
        Assert.That(newPlayer.UserId, Is.EqualTo(1234567));
        Assert.That(newPlayer.BitcoinCount, Is.EqualTo(1000));

        await service.DeletePlayer(createPlayerArgs);
    }

    [Test]
    public async Task AddAndTakeBtc()
    {
        using var channel = _factory.CreateGrpcChannel();
        var service = channel.CreateGrpcService<IPlayerGrpcService>();
        
        var createPlayerArgs = new PlayerIdArgs { PlayerId = 1234567 };
        
        await service.AddNewPlayer(createPlayerArgs);
        
        var newPlayer = await service.GetPlayerById(createPlayerArgs);
        
        Assert.IsNotNull(newPlayer);
        Assert.That(newPlayer.UserId, Is.EqualTo(1234567));
        
        var addBtcPlayerArgs = new PlayerBtcArgs { PlayerId = createPlayerArgs.PlayerId, BitcoinsCount = 1000 };

        await service.AddBitcoinsToPlayer(addBtcPlayerArgs);

        var takeBtcPlayerArgs = new PlayerBtcArgs { PlayerId = createPlayerArgs.PlayerId, BitcoinsCount = 200 };

        await service.TakeBitcoinsFromPlayer(takeBtcPlayerArgs);

        newPlayer = await service.GetPlayerById(createPlayerArgs);
        
        Assert.IsNotNull(newPlayer);
        Assert.That(newPlayer.UserId, Is.EqualTo(1234567));
        Assert.That(newPlayer.BitcoinCount, Is.EqualTo(800));

        await service.DeletePlayer(createPlayerArgs);
    }

    [Test]
    public async Task NotEnoughBtc()
    {
        using var channel = _factory.CreateGrpcChannel();
        var service = channel.CreateGrpcService<IPlayerGrpcService>();
        
        var createPlayerArgs = new PlayerIdArgs { PlayerId = 1234567 };
        
        await service.AddNewPlayer(createPlayerArgs);
        
        var newPlayer = await service.GetPlayerById(createPlayerArgs);
        
        Assert.IsNotNull(newPlayer);
        Assert.That(newPlayer.UserId, Is.EqualTo(1234567));
        
        var addBtcPlayerArgs = new PlayerBtcArgs { PlayerId = createPlayerArgs.PlayerId, BitcoinsCount = 1000 };

        await service.AddBitcoinsToPlayer(addBtcPlayerArgs);

        var takeBtcPlayerArgs = new PlayerBtcArgs { PlayerId = createPlayerArgs.PlayerId, BitcoinsCount = 1100 };

        var ex = Assert.ThrowsAsync<Grpc.Core.RpcException>(async () =>
            await service.TakeBitcoinsFromPlayer(takeBtcPlayerArgs));
        
        Assert.That(ex.Message, Is.EqualTo("Status(StatusCode=\"Unknown\", Detail=\"Exception was thrown by handler. BitcoinOperationException: Error taking 1100 bitcoins from player with Id 1234567: Not Enough Bitcoins\")"));

        await service.DeletePlayer(createPlayerArgs);   
    }
}