using System.Collections.Generic;

public static class TasksInfo
{
    public enum TaskTypes
    {
        Tutorial
    }

    public static Dictionary<TaskTypes, string> TaskIdsMap = new Dictionary<TaskTypes, string>()
    {
        [TaskTypes.Tutorial] = "tutorial"
    };

    public static string GetId(this TaskTypes type)
    {
        return TaskIdsMap[type];
    }
}