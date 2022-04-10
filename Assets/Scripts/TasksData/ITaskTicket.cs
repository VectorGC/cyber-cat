namespace TasksData
{
    public interface ITaskData
    {
        string Id { get; }
        string Name { get; }
        string Description { get; }
        bool IsSolved { get; }
    }
}