using System.Threading.Tasks;
using PlayerService.Repositories.InternalModels;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Dto;
using Shared.Models.Dto.Args;

namespace Shared.Server.Services;

[Service]
public interface IPlayerGrpcService
{
    Task AddNewPlayer(PlayerArgs player);

    Task DeletePlayer(PlayerArgs player);
    
    Task<PlayerDto> GetPlayerById(PlayerArgs player);
}