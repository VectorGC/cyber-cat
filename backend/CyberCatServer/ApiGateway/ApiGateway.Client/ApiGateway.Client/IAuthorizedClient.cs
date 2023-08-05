using ApiGateway.Client.Services;

namespace ApiGateway.Client
{
    public interface IAuthorizedClient
    {
        ITaskRepository Tasks { get; }
        ISolutionService SolutionService { get; }
        IJudgeService JudgeService { get; }
        IPlayerService PlayerService { get; }
    }
}