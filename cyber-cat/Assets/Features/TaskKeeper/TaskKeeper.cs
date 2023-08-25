using System.Collections;
using UnityEngine;

public class TaskKeeper : Interactable
{
    [SerializeField] private TaskType _task;

    public override bool CanInteract => HackerVisionSingleton.Instance.Active;

    public override IEnumerator Interact()
    {
        if (!CanInteract)
        {
            yield break;
        }
        
        yield return CodeEditor.Open(_task.Id());
        yield return new WaitWhile(() => CodeEditor.IsOpen);
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