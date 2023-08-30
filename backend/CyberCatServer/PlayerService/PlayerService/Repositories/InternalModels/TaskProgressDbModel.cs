using Shared.Models.Dto.Data;
using Shared.Models.Enums;

namespace PlayerService.Repositories.InternalModels;

internal class TaskProgressDbModel
{
    public TaskProgressStatus Status { get; set; }

    public TaskData ToData()
    {
        return new TaskData()
        {
            Status = Status
        };
    }
}