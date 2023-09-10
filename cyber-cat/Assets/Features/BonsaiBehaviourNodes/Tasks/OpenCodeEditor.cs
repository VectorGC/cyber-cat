using Bonsai;
using Cysharp.Threading.Tasks;

[BonsaiNode("Tasks/")]
public class OpenCodeEditor : UniTaskNode
{
    protected override async UniTask<Status> RunAsync()
    {
        var task = Blackboard.Get<TaskType>("task");
        await CodeEditor.OpenAsync(task.Id());

        return Status.Success;
    }
}