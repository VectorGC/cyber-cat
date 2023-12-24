using System.Collections.Generic;

public enum TaskType
{
    Tutorial,
    Task1,
    Task2,
    Task3,
    Task4,
    Task5,
    Task6,
    Task7,
    Task8,
    Task9,
}

public static class TaskTypesMap
{
    private static Dictionary<TaskType, string> TaskIdsMap = new Dictionary<TaskType, string>()
    {
        [TaskType.Tutorial] = "tutorial",
        [TaskType.Task1] = "task-1",
        [TaskType.Task2] = "task-2",
        [TaskType.Task3] = "task-3",
        [TaskType.Task4] = "task-4",
        [TaskType.Task5] = "task-5",
        [TaskType.Task6] = "task-6",
        [TaskType.Task7] = "task-7",
        [TaskType.Task8] = "task-8",
        [TaskType.Task9] = "task-9",
    };

    public static string Id(this TaskType type)
    {
        return TaskIdsMap[type];
    }
}