using PlayerService.Repositories;
using Shared.Models.Dto;
using Shared.Models.Dto.Data;
using Shared.Models.Enums;
using Shared.Server.Dto.Args;
using Shared.Server.Exceptions;
using Shared.Server.Models;
using Shared.Server.Services;

namespace PlayerService.GrpcServices;

public class PlayerGrpcService : IPlayerGrpcService
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IJudgeGrpcService _judgeGrpcService;

    public PlayerGrpcService(IPlayerRepository playerRepository, IJudgeGrpcService judgeGrpcService)
    {
        _judgeGrpcService = judgeGrpcService;
        _playerRepository = playerRepository;
    }

    public async Task<PlayerId> AuthorizePlayer(UserId userId)
    {
        var player = await _playerRepository.GetPlayerByUserId(userId);
        if (player == null)
        {
            return await _playerRepository.CreatePlayer(userId);
        }

        return player;
    }

    public async Task RemovePlayer(PlayerId playerId)
    {
        await _playerRepository.RemovePlayer(playerId);
    }

    public async Task<PlayerDto> GetPlayerById(PlayerId playerId)
    {
        var player = await _playerRepository.GetPlayerById(playerId);
        if (player == null)
        {
            throw new PlayerNotFoundException(playerId);
        }

        return player;
    }

    public async Task<VerdictDto> GetVerdict(GetVerdictForPlayerArgs args)
    {
        var verdict = await _judgeGrpcService.GetVerdict(args.SolutionDto);
        switch (verdict.Status)
        {
            case VerdictStatus.Success:
                await _playerRepository.SetTaskStatus(args.PlayerId, args.SolutionDto.TaskId, TaskProgressStatus.Complete);
                break;
            case VerdictStatus.Failure:
                await _playerRepository.SetTaskStatus(args.PlayerId, args.SolutionDto.TaskId, TaskProgressStatus.HaveSolutions);
                break;
        }

        return verdict;
    }

    public async Task<TaskData> GetTaskData(GetTaskDataArgs args)
    {
        return await _playerRepository.GetTaskData(args.PlayerId, args.TaskId);
    }

    public async Task AddBitcoinsToPlayer(GetPlayerBtcArgs playerArgs)
    {
        await _playerRepository.AddBitcoins(playerArgs.PlayerId, playerArgs.BitcoinsAmount);
    }

    public async Task TakeBitcoinsFromPlayer(GetPlayerBtcArgs playerBtcArgs)
    {
        await _playerRepository.RemoveBitcoins(playerBtcArgs.PlayerId, playerBtcArgs.BitcoinsAmount);
    }
}