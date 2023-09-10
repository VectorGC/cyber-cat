using Bonsai;
using Bonsai.Core;
using Cysharp.Threading.Tasks;
using UnityEngine;

[BonsaiNode("Waits/")]
public class WaitToInteractTaskKeeper : Decorator
{
    [SerializeField] private string _hint = "Взаимодействовать - F";

    private HUDController _hudController;
    private UniTask? _asyncTask;

    public override void OnEnter()
    {
        _hudController = FindObjectOfType<HUDController>();
    }

    public override Status Run()
    {
        var keeper = Blackboard.Get<TaskKeeper>("task_keeper");
        var player = Blackboard.Get<Player>("player");

        if (!player.InteractPosibility.CanInteract(keeper))
        {
            return Status.Running;
        }

        _hudController.HintText = _hint;
        if (!player.InteractPosibility.IsPressed)
        {
            return Status.Running;
        }

        // Return what the child returns if it ran, else fail.
        var childStatus = Iterator.LastChildExitStatus.GetValueOrDefault(Status.Success);
        if (childStatus != Status.Success)
        {
            return childStatus;
        }
        
        _asyncTask = keeper.Interact();
        if (!_asyncTask.HasValue)
        {
            return Status.Failure;
        }

        return _asyncTask.Value.Status == UniTaskStatus.Succeeded ? Status.Success : Status.Running;
    }
}