namespace TasksData
{
    public interface ITaskData
    {
        string Id { get; }
        string Name { get; }
        string Description { get; }
        string Output { get; }
        bool? IsSolved { get; }
        float ReceivedScore { get; }
        float TotalScore { get; }
    }
}