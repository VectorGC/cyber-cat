using MongoDB.Bson.Serialization.Attributes;
using Shared.Models.Domain.Tasks;

namespace PlayerService.Domain;

public class TaskProgressEntity
{
    public string TaskId { get; set; }
    public TaskProgressStatusType StatusType { get; set; }
    [BsonIgnoreIfDefault] public string Solution { get; set; }

    public TaskProgress ToDomain()
    {
        return new TaskProgress()
        {
            TaskId = TaskId,
            StatusType = StatusType,
            Solution = Solution
        };
    }
}