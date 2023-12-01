using PlayerService.Domain;
using Shared.Models.Domain.Users;

namespace PlayerService.Application;

public interface IPlayerRepository
{
    Task<PlayerEntity> GetPlayer(UserId userId);
    Task Save(PlayerEntity player);
}