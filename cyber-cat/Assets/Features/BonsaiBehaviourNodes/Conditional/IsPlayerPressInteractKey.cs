using Bonsai;
using Bonsai.Core;

[BonsaiNode("Conditional/Interact/", "IsPlayerPressInteractKey")]
public class IsPlayerPressInteractKey : ConditionalAbort
{
    public override bool Condition()
    {
        var player = Blackboard.Get<Player>("player");
        return player.InteractPosibility.IsPressed;
    }
}