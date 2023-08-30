using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Internal.WebClientAdapters;
using ApiGateway.Client.Services;
using Shared.Models.Dto;

namespace ApiGateway.Client.Internal.Services
{
    internal class PlayerService : IPlayerService
    {
        private readonly Uri _uri;
        private readonly IWebClient _webClient;

        internal PlayerService(Uri uri, IWebClient webClient)
        {
            _uri = uri;
            _webClient = webClient;
        }

        public async Task RemovePlayer()
        {
            await _webClient.DeleteAsync(_uri + "player/delete");
        }

        public async Task<VerdictDto> VerifySolution(string taskId, string sourceCode)
        {
            return await _webClient.PostAsJsonAsync<VerdictDto>(_uri + $"player/verify/{taskId}", new Dictionary<string, string>()
            {
                ["sourceCode"] = sourceCode
            });
        }

        /*
        public async Task AddBitcoins(int bitcoins)
        {
            await _webClient.PutAsync(_uri + "player/bitcoins/add", new Dictionary<string, string>
            {
                ["bitcoins"] = bitcoins.ToString()
            });
        }

        public async Task RemoveBitcoins(int bitcoins)
        {
            await _webClient.PutAsync(_uri + "player/bitcoins/remove", new Dictionary<string, string>
            {
                ["bitcoins"] = bitcoins.ToString()
            });
        }
        */
    }
}