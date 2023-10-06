using MongoDB.Bson.Serialization.Attributes;
using Shared.Models.Data;
using Shared.Models.Enums;

namespace PlayerService.Repositories.InternalModels;

internal class TaskProgressDbModel
{
    public TaskProgressStatus Status { get; set; }
    [BsonIgnoreIfDefault] public string Solution { get; set; }

    public TaskData ToData()
    {
        return new TaskData()
        {
            Status = Status,
            Solution = Solution
        };
    }
}