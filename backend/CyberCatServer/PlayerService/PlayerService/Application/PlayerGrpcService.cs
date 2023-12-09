using System.Net;
using PlayerService.Domain;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Users;
using Shared.Models.Domain.Verdicts;
using Shared.Server.Application.Services;
using Shared.Server.Infrastructure.Exceptions;

namespace PlayerService.Application;

public class PlayerGrpcService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IJudgeService _judgeService;
    private readonly ILogger<PlayerGrpcService> _logger;

    public PlayerGrpcService(IPlayerRepository playerRepository, IJudgeService judgeService, ILogger<PlayerGrpcService> logger)
    {
        _logger = logger;
        _judgeService = judgeService;
        _playerRepository = playerRepository;
    }

    public async Task<List<TaskProgress>> GetTasksProgress(UserId userId)
    {
        var player = await _playerRepository.GetPlayer(userId);
        if (player == null)
            return new List<TaskProgress>();

        var progress = player.Tasks.Values.Select(progress => progress.ToDomain()).ToList();
        return progress;
    }

    public async Task<Verdict> SubmitSolution(SubmitSolutionArgs args)
    {
        var (userId, taskId, solution) = args;
        var verdict = await _judgeService.GetVerdict(new GetVerdictArgs(taskId, solution));

        _logger.LogInformation("{Task} verdict: {Verdict}. Player '{UserId}'", args.TaskId, verdict.ToString(), userId);

        var player = await GetOrCreatePlayer(args.UserId);
        player.SetTaskStatusByVerdict(taskId, verdict, solution);

        var updateResult = await _playerRepository.Update(player);
        if (!updateResult.IsSuccess)
        {
            throw updateResult.Error switch
            {
                _ => new ServiceException("Неизвестная ошибка. Обратитесь к администратору", HttpStatusCode.BadRequest)
            };
        }

        return verdict;
    }

    private async Task<PlayerEntity> GetOrCreatePlayer(UserId userId)
    {
        var player = await _playerRepository.GetPlayer(userId);
        if (player != null)
        {
            return player;
        }

        var createResult = await _playerRepository.Create(userId);
        if (!createResult.IsSuccess)
        {
            throw createResult.Error switch
            {
                _ => new ServiceException("Неизвестная ошибка. Обратитесь к администратору", HttpStatusCode.BadRequest)
            };
        }

        return createResult.Player;
    }

    public async Task RemovePlayer(RemovePlayerArgs args)
    {
        var result = await _playerRepository.Delete(args.UserId);
        if (!result.IsSuccess)
        {
            switch (result.Error)
            {
                case PlayerRepositoryError.NotFound:
                    // Already deleted. ¯\_(ツ)_/¯
                    break;
                default:
                    throw new ServiceException("Неизвестная ошибка. Обратитесь к администратору", HttpStatusCode.BadRequest);
            }
        }
    }

    public async Task<List<UserId>> GetUsersWhoSolvedTask(TaskId taskId)
    {
        var userIds = new List<UserId>();

        var players = _playerRepository.GetPlayerWhoSolvedTask(taskId);
        await foreach (var player in players)
        {
            userIds.Add(player.Id);
        }

        return userIds;
    }
}