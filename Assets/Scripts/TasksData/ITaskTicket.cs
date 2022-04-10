using Cysharp.Threading.Tasks;

namespace TasksData
{
    public interface ITaskData
    {
        int Id { get; }
        string Name { get; }
        string Description { get; }
        bool IsSolved { get; }
    }
}