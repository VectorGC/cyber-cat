namespace Models
{
    public interface ITask
    {
        string Id { get; }
        string Name { get; }
        string Description { get; }
    }
}