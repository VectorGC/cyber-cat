using System;
using Cysharp.Threading.Tasks;
using Legacy_do_not_use_it;
using TasksData;
using UnityEngine;

public class TaskUnitFromFolderView : TaskUnitController
{
    [SerializeField] private TaskUnitFolder taskUnitFolder = new TaskUnitFolder("unit-1", "task-1");

    public override async UniTask<bool> IsTaskSolved(IProgress<float> progress = null)
    {
        return await taskUnitFolder.IsTaskSolved(progress);
    }

    public override async UniTask CallTaskChanged() => await taskUnitFolder.CallTaskChanged();

    public override async UniTask<ITaskData> GetTask(IProgress<float> progress = null)
    {
        return await taskUnitFolder.GetTask(progress);
    }

    public override IDisposable Subscribe(IObserver<ITaskData> observer) => taskUnitFolder.Subscribe(observer);

    public override void OpenCodeEditor()
    {
        CodeEditor.OpenSolution(taskUnitFolder).Forget();
    }
}