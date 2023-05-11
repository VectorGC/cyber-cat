namespace Models
{
    public interface ITasks
    {
        ITask this[string taskId] { get; }
    }
}