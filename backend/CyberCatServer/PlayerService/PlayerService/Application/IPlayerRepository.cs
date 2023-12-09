using PlayerService.Domain;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Users;

namespace PlayerService.Application;

public enum PlayerRepositoryError
{
    None = 0,
    Unknown = 1,
    NotFound
}

public interface IPlayerRepository
{
    Task<CreateResult> Create(UserId userId);
    Task<PlayerEntity> GetPlayer(UserId userId);
    Task<UpdateResult> Update(PlayerEntity player);
    Task<DeleteResult> Delete(UserId userId);
    IAsyncEnumerable<PlayerEntity> GetPlayerWhoSolvedTask(TaskId taskId);
}

public record CreateResult(bool IsSuccess, PlayerRepositoryError Error, PlayerEntity Player);

public record DeleteResult(bool IsSuccess, PlayerRepositoryError Error);

public record UpdateResult(bool IsSuccess, PlayerRepositoryError Error);