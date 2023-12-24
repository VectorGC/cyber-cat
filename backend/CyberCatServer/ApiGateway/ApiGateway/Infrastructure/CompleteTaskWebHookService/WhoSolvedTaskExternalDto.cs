using Faker;
using ProtoBuf;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Users;
using Boolean = Faker.Boolean;

namespace ApiGateway.Infrastructure.CompleteTaskWebHookService;

[ProtoContract]
public class WhoSolvedTaskExternalDto
{
    [ProtoMember(1)] public string TaskId { get; set; }
    [ProtoMember(2)] public string PlayerName { get; set; }
    [ProtoMember(3)] public bool IsSolved { get; set; }

    public static WhoSolvedTaskExternalDto Mock(bool? solved = null)
    {
        var isSolved = solved ?? Boolean.Random();
        return new WhoSolvedTaskExternalDto()
        {
            TaskId = Lorem.Words(1).First(),
            PlayerName = isSolved ? Name.First() : string.Empty,
            IsSolved = isSolved
        };
    }

    public static WhoSolvedTaskExternalDto Solved(TaskId taskId, UserModel userModel)
    {
        return new WhoSolvedTaskExternalDto()
        {
            TaskId = taskId,
            PlayerName = userModel.FirstName,
            IsSolved = true
        };
    }

    public static WhoSolvedTaskExternalDto NotSolved(TaskId taskId)
    {
        return new WhoSolvedTaskExternalDto()
        {
            TaskId = taskId,
            PlayerName = string.Empty,
            IsSolved = false
        };
    }

    public WhoSolvedTaskExternalDto()
    {
    }
}