using Microsoft.AspNetCore.Mvc.Testing;
using ProtoBuf.Grpc.Client;
using Shared.Models.Domain.Users;
using Shared.Server.Ids;
using Shared.Server.Services;
using Shared.Tests;

namespace PlayerService.Tests;

[TestFixture]
[Ignore("Fix issue https://gitlab.com/karim.kimsanbaev/cyber-cat/-/issues/103")]
public class CreateAndRemovePlayerTests
{
    private WebApplicationFactory<Program> _factory;

    [SetUp]
    public void Setup()
    {
        _factory = new WebApplicationFactory<Program>();
    }

    [Test]
    public async Task CreateAndDeleteNewPlayer_WhenPassValidUserId()
    {
        using var channel = _factory.CreateGrpcChannel();
        var service = channel.CreateGrpcService<IPlayerGrpcService>();
        var userId = new UserId(1234567);
        var expectedPlayerId = new PlayerId(1234567);

        var ex = Assert.ThrowsAsync<Grpc.Core.RpcException>(async () => await service.GetPlayerById(expectedPlayerId));
        Assert.That(ex.Message, Is.EqualTo("Status(StatusCode=\"Unknown\", Detail=\"Exception was thrown by handler. PlayerNotFoundException: Player with UserId '1234567' not found\")"));

        var playerId = await service.CreatePlayer(userId);
        Assert.AreEqual(expectedPlayerId, playerId.Value);

        var player = await service.GetPlayerById(playerId);

        Assert.IsNotNull(player);

        await service.RemovePlayer(playerId);

        ex = Assert.ThrowsAsync<Grpc.Core.RpcException>(async () => await service.GetPlayerById(playerId));
        Assert.That(ex.Message, Is.EqualTo("Status(StatusCode=\"Unknown\", Detail=\"Exception was thrown by handler. PlayerNotFoundException: Player with UserId '1234567' not found\")"));
    }
}