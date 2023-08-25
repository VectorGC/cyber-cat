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
    public async Task CreatePlayerAndSomeBtc_WhenPassValidUserId()
    {
        using var channel = _factory.CreateGrpcChannel();
        var service = channel.CreateGrpcService<IPlayerGrpcService>();
        var userId = "1234567";

        await service.CreatePlayer(userId);

        var newPlayer = await service.GetPlayerById(userId);

        Assert.IsNotNull(newPlayer);

        var addBtcPlayerArgs = new PlayerBtcArgs {PlayerId = userId, BitcoinsAmount = 1000};

        await service.AddBitcoinsToPlayer(addBtcPlayerArgs);

        newPlayer = await service.GetPlayerById(userId);
        Assert.IsNotNull(newPlayer);
        Assert.That(newPlayer.BitcoinsAmount, Is.EqualTo(1000));

        await service.DeletePlayer(userId);
    }

    [Test]
    public async Task AddAndRemoveBtc_WhenPassValidUserId()
    {
        using var channel = _factory.CreateGrpcChannel();
        var service = channel.CreateGrpcService<IPlayerGrpcService>();
        var userId = "1234567";

        await service.CreatePlayer(userId);

        var newPlayer = await service.GetPlayerById(userId);

        Assert.IsNotNull(newPlayer);

        var addBtcPlayerArgs = new PlayerBtcArgs {PlayerId = userId, BitcoinsAmount = 1000};

        await service.AddBitcoinsToPlayer(addBtcPlayerArgs);

        var takeBtcPlayerArgs = new PlayerBtcArgs {PlayerId = userId, BitcoinsAmount = 200};

        await service.TakeBitcoinsFromPlayer(takeBtcPlayerArgs);

        newPlayer = await service.GetPlayerById(userId);

        Assert.IsNotNull(newPlayer);
        Assert.That(newPlayer.BitcoinsAmount, Is.EqualTo(800));

        await service.DeletePlayer(userId);
    }

    [Test]
    public async Task NotEnoughBtcError_WhenRemoveOverBtcAmount()
    {
        using var channel = _factory.CreateGrpcChannel();
        var service = channel.CreateGrpcService<IPlayerGrpcService>();
        var userId = "1234567";

        await service.CreatePlayer(userId);

        var newPlayer = await service.GetPlayerById(userId);

        Assert.IsNotNull(newPlayer);

        var addBtcPlayerArgs = new PlayerBtcArgs {PlayerId = userId, BitcoinsAmount = 1000};

        await service.AddBitcoinsToPlayer(addBtcPlayerArgs);

        var takeBtcPlayerArgs = new PlayerBtcArgs {PlayerId = userId, BitcoinsAmount = 1100};

        var ex = Assert.ThrowsAsync<Grpc.Core.RpcException>(async () =>
            await service.TakeBitcoinsFromPlayer(takeBtcPlayerArgs));

        Assert.That(ex.Message, Is.EqualTo("Status(StatusCode=\"Unknown\", Detail=\"Exception was thrown by handler. BitcoinOperationException: Error taking 1100 bitcoins from player with Id 1234567: Not Enough Bitcoins\")"));

        await service.DeletePlayer(userId);
    }
}