using Bonsai;
using Bonsai.Core;

[BonsaiNode("Tasks/Getters/", "GetCurrentActorAsQuestGiverToBlackboard")]
public class GetCurrentActorAsQuestGiverToBlackboard : Task
{
    public override Status Run()
    {
        if (!Tree.actor.TryGetComponent<QuestGiver>(out var questGiver))
        {
            return Status.Failure;
        }

        Blackboard.Set("quest_giver", questGiver);
        return Status.Success;
    }
}