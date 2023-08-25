using Bonsai;
using Bonsai.Core;

[BonsaiNode("Conditional/Interact/", "IsPlayerCanInteractToTaskKeeper")]
public class IsPlayerCanInteractToTaskKeeper : ConditionalAbort
{
    public override bool Condition()
    {
        var keeper = Blackboard.Get<TaskKeeper>("task_keeper");
        var player = Blackboard.Get<Player>("player");

        return player.InteractPosibility.CanInteract(keeper);
    }
}