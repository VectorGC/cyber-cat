using UnityEngine;

public class TaskKeeper : Interactable
{
    [SerializeField] private TaskType _task;

    public override bool CanInteract => HackerVisionSingleton.Instance.Active;

    public override void OnInteract()
    {
        CodeEditor.Open(_task.Id());
    }
}