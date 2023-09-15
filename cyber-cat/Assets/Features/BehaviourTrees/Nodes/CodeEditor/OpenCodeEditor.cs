using System.Text;
using Bonsai;
using Cysharp.Threading.Tasks;
using UnityEngine;

[BonsaiNode("CodeEditor/")]
public class OpenCodeEditor : AsyncTask
{
    [SerializeField] private TaskType _task;

    protected override async UniTask<Status> RunAsync()
    {
        await CodeEditor.OpenAsync(_task.GetId());
        return Status.Success;
    }

    public override void Description(StringBuilder builder)
    {
        base.Description(builder);
        builder.AppendLine($"Task: {_task}");
    }
}