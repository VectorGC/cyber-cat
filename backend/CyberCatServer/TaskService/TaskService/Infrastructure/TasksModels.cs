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
            Tests = new List<TestCaseEntity>()
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
            Tests = new List<TestCaseEntity>()
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
            Tests = new List<TestCaseEntity>()
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
            Tests = new List<TestCaseEntity>()
            {
                new()
                {
                    Inputs = new string[] {"1", "9", "0", "2", "4", "6"},
                    Expected = "Один"
                },
                new()
                {
                    Inputs = new string[] {"2", "6", "6", "2", "4", "8"},
                    Expected = "-"
                },
                new()
                {
                    Inputs = new string[] {"6", "8", "7", "2", "3", "1"},
                    Expected = "Два"
                },
                new()
                {
                    Inputs = new string[] {"9", "1", "1", "2", "4", "2"},
                    Expected = "Один"
                },
                new()
                {
                    Inputs = new string[] {"91", "39", "13", "2", "4", "2"},
                    Expected = "-"
                }
            }
        },
        new()
        {
            Id = "task-4",
            Name = "Перегрузка сети",
            Tests = new List<TestCaseEntity>()
            {
                new()
                {
                    Inputs = new string[] {"serlionfizzqwerty11moon"},
                    Expected = "serlionfizzqwerty11moon"
                },
                new()
                {
                    Inputs = new string[] {"godotfizz5requiem"},
                    Expected = "godotfizzbuzzrequiem"
                },
                new()
                {
                    Inputs = new string[] {"alsoneedubuntu4.65"},
                    Expected = "alsoneedubuntu4.6buzz"
                },
                new()
                {
                    Inputs = new string[] {"color93butany2and1"},
                    Expected = "colorfizzbutany2and1"
                },
                new()
                {
                    Inputs = new string[] {"brutalattack1dot0"},
                    Expected = "brutalattack1dot0"
                }
            }
        }
    };
}