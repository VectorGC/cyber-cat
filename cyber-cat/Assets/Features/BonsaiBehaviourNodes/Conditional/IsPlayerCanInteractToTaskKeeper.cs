using Bonsai;
using Bonsai.Core;
using UnityEngine;

[BonsaiNode("Conditional/Interact/", "IsPlayerCanInteractToTaskKeeper")]
public class IsPlayerCanInteractToTaskKeeper : ConditionalAbort
{
    [SerializeField] public TaskType _task;

    public override bool Condition()
    {
        var keeper = TaskKeeper.FindKeeperForTask(_task);
        var player = FindObjectOfType<Player>();

        return player.CanInteract(keeper);
    }
}