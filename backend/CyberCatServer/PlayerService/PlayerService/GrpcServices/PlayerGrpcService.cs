using PlayerService.Repositories;
using Shared.Models.Dto.Data;
using Shared.Models.Enums;
using Shared.Server.Exceptions.PlayerService;
using Shared.Server.Ids;
using Shared.Server.ProtoHelpers;
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

    public async Task<Response<PlayerId>> CreatePlayer(UserId userId)
    {
        try
        {
            return await _playerRepository.CreatePlayer(userId);
        }
        catch (IdentityPlayerException exception)
        {
            return exception;
        }
    }

    public async Task<Response> RemovePlayer(PlayerId playerId)
    {
        await _playerRepository.RemovePlayer(playerId);
        return new Response();
    }

    public async Task<Response<PlayerId>> GetPlayerByUserId(UserId userId)
    {
        var playerId = await _playerRepository.GetPlayerByUserId(userId);
        if (playerId == null)
        {
            return new PlayerNotFoundException(userId);
        }

        return playerId;
    }

    public async Task<Response<PlayerData>> GetPlayerById(PlayerId playerId)
    {
        var player = await _playerRepository.GetPlayerById(playerId);
        if (player == null)
        {
            throw new PlayerNotFoundException(playerId);
        }

        return player;
    }

    public async Task<Response<VerdictData>> GetVerdict(GetVerdictArgs args)
    {
        var (playerId, taskId, solution) = args;
        var verdict = (VerdictData) await _judgeGrpcService.GetVerdict(args);
        await _playerRepository.SaveCode(playerId, taskId, solution);
        switch (verdict.Status)
        {
            case VerdictStatus.Success:
                await _playerRepository.SetTaskStatus(playerId, taskId, TaskProgressStatus.Complete);
                break;
            case VerdictStatus.Failure:
                await _playerRepository.SetTaskStatus(playerId, taskId, TaskProgressStatus.HaveSolution);
                break;
        }

        return verdict;
    }

    public async Task<Response<TaskData>> GetTaskData(GetTaskDataArgs args)
    {
        return await _playerRepository.GetTaskData(args.PlayerId, args.TaskId);
    }

    public async Task<Response> AddBitcoinsToPlayer(GetPlayerBtcArgs playerArgs)
    {
        await _playerRepository.AddBitcoins(playerArgs.PlayerId, playerArgs.BitcoinsAmount);
        return new Response();
    }

    public async Task<Response> TakeBitcoinsFromPlayer(GetPlayerBtcArgs playerBtcArgs)
    {
        await _playerRepository.RemoveBitcoins(playerBtcArgs.PlayerId, playerBtcArgs.BitcoinsAmount);
        return new Response();
    }
}