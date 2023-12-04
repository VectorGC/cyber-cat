using System.Linq;
using Faker;
using ProtoBuf;
using Shared.Server.Domain;

namespace Shared.Server.Infrastructure;

[ProtoContract]
public class SharedTaskExternalDto
{
    [ProtoMember(1)] public string TaskId { get; set; }
    [ProtoMember(2)] public string PlayerName { get; set; }
    [ProtoMember(3)] public bool IsSolved { get; set; }

    public static SharedTaskExternalDto Mock(bool? solved = null)
    {
        var isSolved = solved ?? Boolean.Random();
        return new SharedTaskExternalDto()
        {
            TaskId = Lorem.Words(1).First(),
            PlayerName = isSolved ? Name.First() : string.Empty,
            IsSolved = isSolved
        };
    }

    public SharedTaskExternalDto(SharedTaskProgress data)
    {
        TaskId = data.TaskId;
        PlayerName = data.UserId.ToString();
        IsSolved = data.Status == SharedTaskStatus.Solved;
    }

    public SharedTaskExternalDto()
    {
    }
}