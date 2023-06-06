namespace Models
{
    public class TaskModel : ITask
    {
        public TaskModel(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; }

        public string Description { get; }
    }
}