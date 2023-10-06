using TaskService.Repositories.InternalModels;

namespace TaskService.Repositories;

internal static class TaskDbModels
{
    public static readonly List<TaskDbModel> Tasks = new()
    {
        new()
        {
            Id = "tutorial",
            Name = "Hello cat!",
            Tests = new TestsDbModel()
            {
                new()
                {
                    Expected = "Hello cat!"
                }
            }
        },
        new()
        {
            Id = "task-1",
            Name = "A + B",
            Tests = new TestsDbModel()
            {
                new()
                {
                    Inputs = new string[] {"1", "1"},
                    Expected = "2"
                },
                new()
                {
                    Inputs = new string[] {"5", "10"},
                    Expected = "15"
                },
                new()
                {
                    Inputs = new string[] {"-1000", "1000"},
                    Expected = "0"
                }
            }
        },
        new()
        {
            Id = "task-2",
            Name = "A + B * C",
            Tests = new TestsDbModel()
            {
                new()
                {
                    Inputs = new string[] {"1", "1", "2"},
                    Expected = "3"
                },
                new()
                {
                    Inputs = new string[] {"5", "10", "8"},
                    Expected = "85"
                },
                new()
                {
                    Inputs = new string[] {"-1000", "1000", "3"},
                    Expected = "2000"
                }
            }
        }
    };
}