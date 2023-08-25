using System.Threading.Tasks;
using Shared.Models.Dto;
using Shared.Models.Dto.Args;

namespace ApiGateway.Client.Services
{
    public interface IPlayerService
    {
        Task CreatePlayer();

        Task RemovePlayer();
    
        Task<PlayerDto> GetPlayer();

        Task AddBitcoins(int bitcoins);

        Task RemoveBitcoins(int bitcoins);
    }
}