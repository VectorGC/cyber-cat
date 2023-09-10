using Bonsai;
using Bonsai.Core;

[BonsaiNode("Conditional/Interact/", "IsPlayerCanInteractToQuestGiver")]
public class IsPlayerCanInteractToQuestGiver : ConditionalAbort
{
    public override bool Condition()
    {
        var questGiver = Blackboard.Get<QuestGiver>("quest_giver");
        var player = Blackboard.Get<Player>("player");

        return false;
        // return player.InteractPosibility.CanInteract(questGiver);
    }
}