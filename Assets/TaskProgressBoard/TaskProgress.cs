using TasksData;

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