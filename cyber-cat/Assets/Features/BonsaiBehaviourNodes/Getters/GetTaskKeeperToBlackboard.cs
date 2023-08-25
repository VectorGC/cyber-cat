using System.Text;
using Bonsai;
using Bonsai.Core;
using UnityEngine;

[BonsaiNode("Tasks/Getters/", "GetTaskKeeperToBlackboard")]
public class GetTaskKeeperToBlackboard : Task
{
    [SerializeField] private TaskType _task;

    public override Status Run()
    {
        var keeper = TaskKeeper.FindKeeperForTask(_task);
        Blackboard.Set("task_keeper", keeper);

        return Status.Success;
    }

    public override void Description(StringBuilder builder)
    {
        base.Description(builder);
        builder.AppendLine($"Task: {_task}");
    }
}