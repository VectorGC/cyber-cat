using PlayerService.Domain;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Users;

namespace PlayerService.Application;

public enum UserRepositoryError
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

public record CreateResult(bool IsSuccess, UserRepositoryError Error, PlayerEntity Player);

public record DeleteResult(bool IsSuccess, UserRepositoryError Error);

public record UpdateResult(bool IsSuccess, UserRepositoryError Error);