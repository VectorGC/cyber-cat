using Bonsai;
using Bonsai.Core;

[BonsaiNode("Tasks/Getters/", "GetPlayerToBlackboard")]
public class GetPlayerToBlackboard : Task
{
    public override Status Run()
    {
        var player = FindObjectOfType<Player>();
        Blackboard.Set("player", player);

        return Status.Success;
    }
}