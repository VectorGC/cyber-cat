using Cysharp.Threading.Tasks;
using UnityEngine;

public class TaskKeeper : Interactable
{
    [SerializeField] private TaskType _task;

    public override bool CanInteract => HackerVisionSingleton.Instance.Active;

    protected override async UniTask OnInteract()
    {
        await CodeEditor.OpenAsync(_task.Id());
        await UniTask.WaitWhile(() => CodeEditor.IsOpen);
    }

    private static TaskKeeper[] _keepersCache;

    public static TaskKeeper FindKeeperForTask(TaskType task)
    {
        _keepersCache ??= FindObjectsOfType<TaskKeeper>();

        foreach (var keeper in _keepersCache)
        {
            if (keeper._task == task)
            {
                return keeper;
            }
        }

        return null;
    }
}