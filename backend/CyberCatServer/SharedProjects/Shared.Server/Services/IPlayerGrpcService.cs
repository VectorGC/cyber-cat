using System.Threading.Tasks;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Dto;
using Shared.Models.Dto.Args;

namespace Shared.Server.Services;

[Service]
public interface IPlayerGrpcService
{
    Task AddNewPlayer(PlayerIdArgs playerId);

    Task DeletePlayer(PlayerIdArgs playerId);
    
    Task<PlayerDto> GetPlayerById(PlayerIdArgs playerId);

    Task AddBitcoinsToPlayer(PlayerBtcArgs playerBtcArgs);

    Task TakeBitcoinsFromPlayer(PlayerBtcArgs playerBtcArgs);
}