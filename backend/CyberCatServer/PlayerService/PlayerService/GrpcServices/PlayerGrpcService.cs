using PlayerService.Repositories;
using Shared.Models.Dto;
using Shared.Models.Dto.Args;
using Shared.Models.Dto.ProtoHelpers;
using Shared.Server.Services;

namespace PlayerService.GrpcServices;

public class PlayerGrpcService : IPlayerGrpcService
{
    private readonly IPlayerRepository _playerRepository;

    public PlayerGrpcService(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task CreatePlayer(StringProto userId)
    {
        await _playerRepository.CreatePlayer(userId);
    }

    public async Task DeletePlayer(StringProto id)
    {
        await _playerRepository.RemovePlayer(id);
    }

    public async Task<PlayerDto> GetPlayerById(StringProto id)
    {
        var player = await _playerRepository.GetPlayerById(id);
        return player;
    }

    public async Task AddBitcoinsToPlayer(PlayerBtcArgs playerArgs)
    {
        await _playerRepository.AddBitcoins(playerArgs.PlayerId, playerArgs.BitcoinsAmount);
    }

    public async Task TakeBitcoinsFromPlayer(PlayerBtcArgs playerBtcArgs)
    {
        await _playerRepository.RemoveBitcoins(playerBtcArgs.PlayerId, playerBtcArgs.BitcoinsAmount);
    }
}