using System;
using System.Threading.Tasks;
using ApiGateway.Client.Internal.Tasks;
using ApiGateway.Client.Internal.WebClientAdapters;
using ApiGateway.Client.Models;

namespace ApiGateway.Client.Internal.Players
{
    internal class PlayerClientProxy : IPlayer
    {
        public ITaskRepository Tasks => _tasks;

        private readonly Uri _uri;
        private readonly IWebClient _webClient;
        private readonly TaskRepositoryProxy _tasks;

        public static async Task<PlayerClientProxy> Create(Uri uri, IWebClient webClient)
        {
            var client = new PlayerClientProxy(uri, webClient);
            await client.Init();

            return client;
        }

        private async Task Init()
        {
            await _tasks.Init();
        }

        private PlayerClientProxy(Uri uri, IWebClient webClient)
        {
            _uri = uri;
            _webClient = webClient;
            _tasks = new TaskRepositoryProxy(uri, webClient);
        }

        public async Task Remove()
        {
            await _webClient.PostAsync(_uri + "player/remove");
        }
    }
}