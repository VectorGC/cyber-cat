using System.Net;
using AutoMapper;
using PlayerService.Domain;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Users;
using Shared.Models.Domain.VerdictHistory;
using Shared.Models.Domain.Verdicts;
using Shared.Server.Application.Services;
using Shared.Server.Infrastructure.Exceptions;

namespace PlayerService.Application;

public class PlayerGrpcService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IJudgeService _judgeService;
    private readonly ILogger<PlayerGrpcService> _logger;
    private readonly IMapper _mapper;

    public PlayerGrpcService(IPlayerRepository playerRepository, IJudgeService judgeService, ILogger<PlayerGrpcService> logger, IMapper mapper)
    {
        _mapper = mapper;
        _logger = logger;
        _judgeService = judgeService;
        _playerRepository = playerRepository;
    }

    public async Task<List<TaskProgress>> GetTasksProgress(UserId userId)
    {
        var player = await _playerRepository.GetPlayer(userId);
        if (player == null)
            return new List<TaskProgress>();

        var progress = GetTasksProgress(player);
        return progress;
    }

    private List<TaskProgress> GetTasksProgress(PlayerEntity playerEntity)
    {
        var tasksProgress = new List<TaskProgress>();
        foreach (var verdict in GetBestOrLastVerdictForAllTasks(playerEntity.Verdicts))
        {
            var progress = new TaskProgress()
            {
                Solution = verdict.Solution,
                StatusType = verdict.IsSuccess ? TaskProgressStatusType.Complete : TaskProgressStatusType.HaveSolution,
                TaskId = verdict.TaskId
            };
            tasksProgress.Add(progress);
        }

        return tasksProgress;
    }

    private IEnumerable<VerdictEntity> GetBestOrLastVerdictForAllTasks(List<VerdictEntity> verdictEntities)
    {
        var taskIds = new HashSet<TaskId>(verdictEntities.Select(v => new TaskId(v.TaskId)));
        foreach (var id in taskIds)
        {
            var successVerdict = verdictEntities.Where(v => v.TaskId == id).FirstOrDefault(v => v.IsSuccess);
            if (successVerdict != null)
            {
                yield return successVerdict;
                continue;
            }

            var lastVerdict = verdictEntities.MaxBy(v => v.DateTime);
            yield return lastVerdict;
        }
    }


    public async Task<Verdict> SubmitSolution(SubmitSolutionArgs args)
    {
        var (userId, taskId, solution) = args;
        var verdict = await _judgeService.GetVerdict(new GetVerdictArgs(taskId, solution));

        _logger.LogInformation("{Task} verdict: {Verdict}. Player '{UserId}'", args.TaskId, verdict.ToString(), userId);

        var player = await GetOrCreatePlayer(args.UserId);
        var verdictEntity = _mapper.Map<VerdictEntity>(verdict);
        player.Verdicts.Add(verdictEntity);

        var updateResult = await _playerRepository.Update(player);
        if (!updateResult.IsSuccess)
        {
            throw updateResult.Error switch
            {
                _ => throw new ServiceException("Неизвестная ошибка. Обратитесь к администратору", HttpStatusCode.BadRequest)
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
                _ => throw new ServiceException("Неизвестная ошибка. Обратитесь к администратору", HttpStatusCode.BadRequest)
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
        var players = await _playerRepository.GetPlayerWhoSolvedTask(taskId);
        var userIds = players.Select(p => new UserId(p.Id)).ToList();
        return userIds;
    }

    public async Task SaveVerdictHistory(SaveVerdictHistoryArgs args)
    {
        var (userId, verdicts) = args;

        var verdictEntities = _mapper.Map<List<VerdictEntity>>(verdicts);
        var player = await GetOrCreatePlayer(userId);
        player.Verdicts.AddRange(verdictEntities);

        var updateResult = await _playerRepository.Update(player);
        if (!updateResult.IsSuccess)
        {
            throw updateResult.Error switch
            {
                _ => throw new ServiceException("Неизвестная ошибка. Обратитесь к администратору", HttpStatusCode.BadRequest)
            };
        }
    }
}