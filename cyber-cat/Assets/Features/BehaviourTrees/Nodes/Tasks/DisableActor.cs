using Bonsai;
using Bonsai.Core;

[BonsaiNode("Tasks/")]
public class DisableActor : Task
{
    public override Status Run()
    {
        Tree.actor.SetActive(false);
        return Status.Success;
    }
}