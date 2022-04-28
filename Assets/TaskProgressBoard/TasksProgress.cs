using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using TasksData;
using TaskUnits;

public class TasksProgress
{
    public static async UniTask<TasksProgress> GetFromServer(string token, IProgress<float> progress = null)
    {
        var tasks = await TaskFacade.GetAllTasks(token, progress);
        var tasksProgress = GetTasksProgress(tasks);

        return new TasksProgress(tasksProgress);
    }

    private static IEnumerable<TaskProgress> GetTasksProgress(IEnumerable<ITaskData> tasksData)
    {
        foreach (var task in tasksData)
        {
            var taskProgress = new TaskProgress(task);
            yield return taskProgress;
        }
    }

    private TasksProgress(IEnumerable<TaskProgress> tasks)
    {
        Tasks = new List<TaskProgress>(tasks);
    }

    public override string ToString()
    {
        var index = 0;
        var sb = new StringBuilder();
        foreach (var task in Tasks)
        {
            index++;
            if (task.ReceivedScore >= task.TotalScore)
            {
                sb.AppendLine($"<color=green>{index}. {task}</color>");
                continue;
            }

            sb.AppendLine($"{index}. {task}");
        }

        return sb.ToString();
    }

    public IReadOnlyList<TaskProgress> Tasks { get; }
}