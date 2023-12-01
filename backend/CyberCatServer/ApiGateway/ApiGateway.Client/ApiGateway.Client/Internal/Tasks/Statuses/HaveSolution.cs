namespace ApiGateway.Client.Internal.Tasks.Statuses
{
    public class HaveSolution : ITaskProgressStatus
    {
        public string Solution { get; }

        public HaveSolution(string solution)
        {
            Solution = solution;
        }
    }
}