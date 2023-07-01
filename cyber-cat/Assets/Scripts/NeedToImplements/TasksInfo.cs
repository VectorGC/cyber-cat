using System.Collections.Generic;

public static class TasksInfo
{
    public enum TaskTypes
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

    public static Dictionary<TaskTypes, string> TaskIdsMap = new Dictionary<TaskTypes, string>()
    {
        [TaskTypes.Tutorial] = "tutorial",
        [TaskTypes.Task1] = "task-1",
        [TaskTypes.Task2] = "task-2",
        [TaskTypes.Task3] = "task-3",
        [TaskTypes.Task4] = "task-4",
        [TaskTypes.Task5] = "task-5",
        [TaskTypes.Task6] = "task-6",
        [TaskTypes.Task7] = "task-7",
        [TaskTypes.Task8] = "task-8",
        [TaskTypes.Task9] = "task-9",
    };

    public static string GetId(this TaskTypes type)
    {
        return TaskIdsMap[type];
    }
}