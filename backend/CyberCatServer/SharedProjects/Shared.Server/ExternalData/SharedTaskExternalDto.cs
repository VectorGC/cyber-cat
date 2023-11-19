using Shared.Server.Data;
using TaskService.Repositories;

namespace Shared.Server.ExternalData;

public class SharedTaskExternalDto
{
    public string TaskId { get; set; }
    public string PlayerName { get; set; }
    public bool IsSolved { get; set; }

    public SharedTaskExternalDto(SharedTaskProgressData data)
    {
        TaskId = data.Id;
        PlayerName = data.PlayerIdData.ToString();
        IsSolved = data.Status == SharedTaskStatus.Solved;
    }

    public SharedTaskExternalDto()
    {
    }
}