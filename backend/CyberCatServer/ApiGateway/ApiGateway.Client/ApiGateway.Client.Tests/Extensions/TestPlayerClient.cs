using System;
using ApiGateway.Client.Application;

namespace ApiGateway.Client.Tests.Extensions
{
    public class TestPlayerClient : IDisposable
    {
        public ApiGatewayClient Client { get; }

        public TestPlayerClient(ApiGatewayClient client)
        {
            Client = client;
        }

        public void Dispose()
        {
            Client.Player.Remove();
            Client.Dispose();
        }
    }
}