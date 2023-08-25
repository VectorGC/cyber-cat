using Microsoft.AspNetCore.Mvc.Testing;
using ProtoBuf.Grpc.Client;
using Shared.Models.Dto.Args;
using Shared.Server.Services;
using Shared.Tests;

namespace PlayerService.Tests;

[TestFixture]
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
        var userId = "1234567";

        var ex = Assert.ThrowsAsync<Grpc.Core.RpcException>(async () => await service.GetPlayerById(userId));

        Assert.That(ex.Message, Is.EqualTo("Status(StatusCode=\"Unknown\", Detail=\"Exception was thrown by handler. PlayerNotFoundException: Player with UserId '1234567' not found\")"));

        await service.CreatePlayer(userId);

        var newPlayer = await service.GetPlayerById(userId);

        Assert.IsNotNull(newPlayer);

        await service.DeletePlayer(userId);

        ex = Assert.ThrowsAsync<Grpc.Core.RpcException>(async () => await service.GetPlayerById(userId));
        Assert.That(ex.Message, Is.EqualTo("Status(StatusCode=\"Unknown\", Detail=\"Exception was thrown by handler. PlayerNotFoundException: Player with UserId '1234567' not found\")"));
    }
}