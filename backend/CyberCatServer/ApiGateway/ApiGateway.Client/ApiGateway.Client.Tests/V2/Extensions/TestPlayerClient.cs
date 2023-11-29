using System;
using ApiGateway.Client.V3.Application;
using ApiGateway.Client.V3.Domain;

namespace ApiGateway.Client.Tests.V2.Extensions
{
    public class TestPlayerClient : IDisposable
    {
        public PlayerModel Player => Client.Player;
        public ApiGatewayClient Client { get; }

        public TestPlayerClient(ApiGatewayClient client)
        {
            Client = client;
        }

        public void Dispose()
        {
            Client.PlayerService.Remove();
            Client.Dispose();
        }
    }
}