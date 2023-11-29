using PlayerService.Repositories;
using Shared.Models.Data;
using Shared.Models.Domain.Players;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Users;
using Shared.Models.Domain.Verdicts;
using Shared.Models.Enums;
using Shared.Server.Exceptions.PlayerService;
using Shared.Server.ProtoHelpers;
using Shared.Server.Services;

namespace PlayerService.GrpcServices;

public class PlayerGrpcService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IJudgeService _judgeService;
    private readonly ITaskService _taskService;
    private readonly ILogger<PlayerGrpcService> _logger;

    public PlayerGrpcService(IPlayerRepository playerRepository, IJudgeService judgeService, ITaskService taskService, ILogger<PlayerGrpcService> logger)
    {
        _logger = logger;
        _taskService = taskService;
        _judgeService = judgeService;
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

    public async Task<Response<Verdict>> GetVerdict(GetVerdictArgs args)
    {
        var (playerId, taskId, solution) = args;
        var verdict = await _judgeService.GetVerdict(args);
        await _playerRepository.SaveCode(playerId, taskId, solution);
        _logger.LogInformation("{Task} verdict: {Verdict}. Player '{Player}'", args.TaskId, verdict.Value.ToString(), args.PlayerId);
        switch (verdict.Value)
        {
            case Success success:
                await _playerRepository.SetTaskStatus(playerId, taskId, TaskProgressStatus.Complete);
                await _taskService.OnTaskSolved(new OnTaskSolvedArgs(playerId, taskId));
                break;
            default:
                await _playerRepository.SetTaskStatus(playerId, taskId, TaskProgressStatus.HaveSolution);
                break;
        }

        return verdict;
    }

    public async Task<Response<TaskProgressData>> GetTaskData(GetTaskDataArgs args)
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