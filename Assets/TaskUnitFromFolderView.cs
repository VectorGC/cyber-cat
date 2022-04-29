using System;
using System.Collections.Generic;
using Authentication;
using Cysharp.Threading.Tasks;
using Legacy_do_not_use_it;
using TasksData;
using UniRx;
using UnityEngine;

public class TaskUnitFromFolderView : MonoBehaviour, ITaskUnit
{
    [SerializeField] private TaskUnitFolder taskUnitFolder = new TaskUnitFolder("unit-1", "task-1");

    [SerializeField] private List<MonoBehaviourObserver<ITaskData>> taskDataObservers;

    private ITaskUnit _taskUnit;

    private async void Start()
    {
        var token = TokenSession.FromPlayerPrefs();
        var taskUnit = await TaskUnit.CreateTaskUnit(token, taskUnitFolder);

        var observable = taskUnit.StartWith(taskUnit); // All observers received data.
        foreach (var observer in taskDataObservers)
        {
            observable.Subscribe(observer);
        }

        _taskUnit = taskUnit;
    }

    public UniTask<ITaskData> GetTask(string token, IProgress<float> progress = null) =>
        _taskUnit.GetTask(token, progress);

    public UniTask<bool?> IsTaskSolved(string token, IProgress<float> progress = null) =>
        _taskUnit.IsTaskSolved(token, progress);
}