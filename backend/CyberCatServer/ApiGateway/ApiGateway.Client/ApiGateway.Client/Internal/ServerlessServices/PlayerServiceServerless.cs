using System.Threading.Tasks;
using ApiGateway.Client.Services;
using Shared.Models.Dto;
using Shared.Models.Enums;

namespace ApiGateway.Client.Internal.ServerlessServices
{
    public class PlayerServiceServerless : IPlayerService
    {
        public Task<PlayerDto> GetPlayer()
        {
            return Task.FromResult<PlayerDto>(new PlayerDto());
        }

        public Task<VerdictDto> VerifySolution(string taskId, string sourceCode)
        {
            var verdict = new VerdictDto()
            {
                Status = VerdictStatus.Success
            };
            return Task.FromResult<VerdictDto>(verdict);
        }

        public Task AddBitcoins(int bitcoins)
        {
            return Task.CompletedTask;
        }

        public Task RemoveBitcoins(int bitcoins)
        {
            return Task.CompletedTask;
        }
    }
}