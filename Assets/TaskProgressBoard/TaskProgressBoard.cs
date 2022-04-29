using System;
using System.Collections.Generic;
using System.Text;
using Authentication;
using Cysharp.Threading.Tasks;
using RestAPIWrapper;
using TasksData;
using UnityEngine;

public class TasksProgress
{
    public static async UniTask<TasksProgress> GetFromServer(string token, IProgress<float> progress = null)
    {
        var tasks = await RestAPI.GetTasks(token, progress);
        var tasksProgress = GetTasksProgress(tasks.Values);

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

    public TasksProgress(IEnumerable<TaskProgress> tasks)
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

public class TaskProgress
{
    public string TaskName { get; }
    public float ReceivedScore { get; }
    public float TotalScore { get; }

    public TaskProgress(ITaskData taskData)
    {
        TaskName = taskData.Name;
        ReceivedScore = taskData.ReceivedScore;
        TotalScore = taskData.TotalScore;
    }

    public override string ToString() => $"{TaskName} - {ReceivedScore} / {TotalScore}";
}

public static class TaskProgressBoard
{
    public static async UniTask ShowTaskProgressBoard()
    {
        var token = TokenSession.FromPlayerPrefs();
        var tasksProgress = await TasksProgress.GetFromServer(token);

        OnTaskProgressReceived(tasksProgress);
    }

    private static void OnTaskProgressReceived(TasksProgress tasksProgress)
    {
        Time.timeScale = 0f;
        var modalPanel = UnityEngine.Object.FindObjectOfType<ModalPanel>();
        modalPanel.MessageBox(null, "Прогресс по задачам", tasksProgress.ToString(), () => { }, () => { }, () => { },
            () => { Time.timeScale = 1f; }, false, "Ok");
    }
}