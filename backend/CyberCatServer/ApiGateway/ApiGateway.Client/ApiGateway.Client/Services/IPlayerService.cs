using System.Threading.Tasks;
using Shared.Models.Dto;
using Shared.Models.Dto.Args;

namespace ApiGateway.Client.Services
{
    public interface IPlayerService
    {
        Task AddNewPlayer();

        Task DeletePlayer();
    
        Task<PlayerDto> GetPlayerById();

        Task AddBitcoinsToPlayer(int bitcoins);

        Task TakeBitcoinsFromPlayer(int bitcoins);
    }
}