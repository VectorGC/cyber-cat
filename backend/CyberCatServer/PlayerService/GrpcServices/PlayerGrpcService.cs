using PlayerService.Repositories;
using PlayerService.Repositories.InternalModels;
using Shared.Models.Dto;
using Shared.Models.Dto.Args;
using Shared.Server.Services;

namespace PlayerService.GrpcServices;

public class PlayerGrpcService : IPlayerGrpcService
{
    private readonly IPlayerRepository _playerRepository;

    public PlayerGrpcService(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }
    public async Task AddNewPlayer(PlayerArgs args)
    {
        await _playerRepository.AddNewPlayer(args.UserId);
    }
    public async Task DeletePlayer(PlayerArgs args)
    {
        await _playerRepository.DeletePlayer(args.UserId);
    }
    public async Task<PlayerDto> GetPlayerById(PlayerArgs args)
    {
        var player = await _playerRepository.GetPlayerById(args.UserId);
        return player;
    }
}