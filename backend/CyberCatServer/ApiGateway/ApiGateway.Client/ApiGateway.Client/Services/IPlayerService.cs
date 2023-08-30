using System.Threading.Tasks;
using Shared.Models.Dto;

namespace ApiGateway.Client.Services
{
    public interface IPlayerService
    {
        Task<VerdictDto> VerifySolution(string taskId, string sourceCode);
        // Task AddBitcoins(int bitcoins);
        // Task RemoveBitcoins(int bitcoins);
    }
}