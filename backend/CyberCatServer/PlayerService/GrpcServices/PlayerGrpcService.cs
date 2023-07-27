using PlayerService.Repositories;
using PlayerService.Repositories.InternalModels;
using Shared.Models.Dto;
using Shared.Models.Dto.Args;
using Shared.Server.Exceptions;
using Shared.Server.Services;

namespace PlayerService.GrpcServices;

public class PlayerGrpcService : IPlayerGrpcService
{
    private readonly IPlayerRepository _playerRepository;

    public PlayerGrpcService(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }
    public async Task AddNewPlayer(PlayerIdArgs idArgs)
    {
        await _playerRepository.AddNewPlayer(idArgs.PlayerId);
    }
    public async Task DeletePlayer(PlayerIdArgs idArgs)
    {
        await _playerRepository.DeletePlayer(idArgs.PlayerId);
    }
    public async Task<PlayerDto> GetPlayerById(PlayerIdArgs idArgs)
    {
        var player = await _playerRepository.GetPlayerById(idArgs.PlayerId);
        return player;
    }
    public async Task AddBitcoinsToPlayer(PlayerBtcArgs playerArgs)
    {
        await _playerRepository.AddBitcoinsToPlayer(playerArgs.PlayerId, playerArgs.BitcoinsCount);
    }

    public async Task TakeBitcoinsFromPlayer(PlayerBtcArgs playerBtcArgs)
    {
        await _playerRepository.TakeBitcoinsFromPlayer(playerBtcArgs.PlayerId, playerBtcArgs.BitcoinsCount);
    }
}