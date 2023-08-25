using System.Threading.Tasks;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Dto;
using Shared.Models.Dto.Args;
using Shared.Models.Dto.ProtoHelpers;

namespace Shared.Server.Services;

[Service]
public interface IPlayerGrpcService
{
    Task CreatePlayer(StringProto userId);

    Task DeletePlayer(StringProto userId);

    Task<PlayerDto> GetPlayerById(StringProto userId);

    Task AddBitcoinsToPlayer(PlayerBtcArgs playerBtcArgs);

    Task TakeBitcoinsFromPlayer(PlayerBtcArgs playerBtcArgs);
}