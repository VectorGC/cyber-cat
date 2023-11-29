using MongoDB.Bson.Serialization.Attributes;
using Shared.Models.Data;
using Shared.Models.Domain.Tasks;
using Shared.Models.Enums;

namespace PlayerService.Repositories.InternalModels;

internal class TaskProgressDbModel
{
    public TaskProgressStatus Status { get; set; }
    [BsonIgnoreIfDefault] public string Solution { get; set; }

    public TaskProgressData ToData()
    {
        return new TaskProgressData()
        {
            Status = Status,
            Solution = Solution
        };
    }
}