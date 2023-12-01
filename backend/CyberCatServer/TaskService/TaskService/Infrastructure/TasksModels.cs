using TaskService.Domain;

namespace TaskService.Infrastructure;

internal static class TasksModels
{
    public static readonly List<TaskEntity> Tasks = new()
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
            IsShared = true,
            Tests = new TestsDbModel()
            {
                new()
                {
                    Inputs = new string[] {"1", "1", "2"},
                    Expected = "Пароль: 3"
                },
                new()
                {
                    Inputs = new string[] {"5", "10", "8"},
                    Expected = "Пароль: 85"
                },
                new()
                {
                    Inputs = new string[] {"-1000", "1000", "3"},
                    Expected = "Пароль: 2000"
                }
            }
        },
        new()
        {
            Id = "task-3",
            Name = "Шкафчик с паролем",
            Tests = new TestsDbModel()
            {
                new()
                {
                    Inputs = new string[] {"1", "1", "2"},
                    Expected = "Пароль: 3"
                },
                new()
                {
                    Inputs = new string[] {"5", "10", "8"},
                    Expected = "Пароль: 85"
                },
                new()
                {
                    Inputs = new string[] {"-1000", "1000", "3"},
                    Expected = "Пароль: 2000"
                }
            }
        }
    };
}