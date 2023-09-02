using Microsoft.AspNetCore.Mvc.Testing;

namespace PlayerService.Tests;

[TestFixture]
[Ignore("Fix issue https://gitlab.com/karim.kimsanbaev/cyber-cat/-/issues/103")]
public class BitcoinsTests
{
    private WebApplicationFactory<Program> _factory;

    [SetUp]
    public void Setup()
    {
        _factory = new WebApplicationFactory<Program>();
    }

    /*
    [Test]
    public async Task CreatePlayerAndSomeBtc_WhenPassValidUserId()
    {
        using var channel = _factory.CreateGrpcChannel();
        var service = channel.CreateGrpcService<IPlayerGrpcService>();
        var userId = new UserId(1234567);

        var response = await service.AuthorizePlayer(userId);
        var playerId = response.PlayerId;
        var player = await service.GetPlayerById(playerId);

        Assert.IsNotNull(player);

        var addBtcPlayerArgs = new GetPlayerBtcArgs {PlayerId = playerId, BitcoinsAmount = 1000};

        await service.AddBitcoinsToPlayer(addBtcPlayerArgs);

        player = await service.GetPlayerById(playerId);
        Assert.IsNotNull(player);
        Assert.That(player.BitcoinsAmount, Is.EqualTo(1000));

        await service.RemovePlayer(playerId);
    }

    [Test]
    public async Task AddAndRemoveBtc_WhenPassValidUserId()
    {
        using var channel = _factory.CreateGrpcChannel();
        var service = channel.CreateGrpcService<IPlayerGrpcService>();
        var userId = new UserId(1234567);

        var response = await service.AuthorizePlayer(userId);
        var playerId = response.PlayerId;
        var player = await service.GetPlayerById(playerId);

        Assert.IsNotNull(player);

        var addBtcPlayerArgs = new GetPlayerBtcArgs {PlayerId = playerId, BitcoinsAmount = 1000};

        await service.AddBitcoinsToPlayer(addBtcPlayerArgs);

        var takeBtcPlayerArgs = new GetPlayerBtcArgs {PlayerId = playerId, BitcoinsAmount = 200};

        await service.TakeBitcoinsFromPlayer(takeBtcPlayerArgs);

        player = await service.GetPlayerById(playerId);

        Assert.IsNotNull(player);
        Assert.That(player.BitcoinsAmount, Is.EqualTo(800));

        await service.RemovePlayer(playerId);
    }

    [Test]
    public async Task NotEnoughBtcError_WhenRemoveOverBtcAmount()
    {
        using var channel = _factory.CreateGrpcChannel();
        var service = channel.CreateGrpcService<IPlayerGrpcService>();
        var userId = new UserId(1234567);

        await service.AuthorizePlayer(userId);

        var response = await service.AuthorizePlayer(userId);
        var playerId = response.PlayerId;
        var player = await service.GetPlayerById(playerId);

        Assert.IsNotNull(player);

        var addBtcPlayerArgs = new GetPlayerBtcArgs {PlayerId = playerId, BitcoinsAmount = 1000};

        await service.AddBitcoinsToPlayer(addBtcPlayerArgs);

        var takeBtcPlayerArgs = new GetPlayerBtcArgs {PlayerId = playerId, BitcoinsAmount = 1100};

        var ex = Assert.ThrowsAsync<Grpc.Core.RpcException>(async () =>
            await service.TakeBitcoinsFromPlayer(takeBtcPlayerArgs));

        Assert.That(ex.Message, Is.EqualTo("Status(StatusCode=\"Unknown\", Detail=\"Exception was thrown by handler. BitcoinOperationException: Error taking 1100 bitcoins from player with Id 1234567: Not Enough Bitcoins\")"));

        await service.RemovePlayer(playerId);
    }
    */
}