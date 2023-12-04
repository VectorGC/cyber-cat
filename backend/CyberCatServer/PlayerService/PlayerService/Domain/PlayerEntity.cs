using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Users;
using Shared.Models.Domain.Verdicts;

namespace PlayerService.Domain;

[CollectionName("Players")]
public class PlayerEntity : IDocument<long>
{
    public long Id { get; set; }
    public Dictionary<string, TaskProgressEntity> Tasks { get; set; } = new Dictionary<string, TaskProgressEntity>();

    public int Version { get; set; }

    public PlayerEntity(UserId userId)
    {
        Id = userId.Value;
    }

    public PlayerEntity()
    {
    }

    public void SetTaskStatusByVerdict()
    {
        throw new NotImplementedException();
    }

    public void SetTaskStatusByVerdict(TaskId taskId, Verdict verdict, string solution)
    {
        if (!Tasks.ContainsKey(taskId))
            Tasks[taskId] = new TaskProgressEntity();

        Tasks[taskId].StatusType = verdict switch
        {
            Success success => TaskProgressStatusType.Complete,
            _ => TaskProgressStatusType.HaveSolution
        };

        Tasks[taskId].Solution = solution;
    }
}