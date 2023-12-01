using MongoDB.Bson.Serialization.Attributes;
using Shared.Models.Domain.Tasks;

namespace PlayerService.Domain;

public class TaskProgressEntity
{
    public TaskProgressStatusType StatusType { get; set; }
    [BsonIgnoreIfDefault] public string Solution { get; set; }

    public TaskProgress ToDomain()
    {
        return new TaskProgress()
        {
            StatusType = StatusType,
            Solution = Solution
        };
    }
}