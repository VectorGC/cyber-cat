using System;
using System.Collections.Generic;
using Authentication;
using TaskUnits;
using TaskUnits.Messages;
using UnityEngine;

public class TaskUnitFromFolderView : MonoBehaviour, IObservable<ITaskData>
{
    [SerializeField] private TaskUnitFolder taskUnitFolder = new TaskUnitFolder("unit-1", "task-1");

    [SerializeField] private List<MonoBehaviourObserver<ITaskData>> taskDataObservers;

    private IObservable<ITaskData> _observableTask;

    private async void Start()
    {
        var token = TokenSession.FromPlayerPrefs();

        var task = await taskUnitFolder.GetTask(token);

        _observableTask = task.ToObservable();
        foreach (var observer in taskDataObservers)
        {
            _observableTask.Subscribe(observer);
        }
    }

    public IDisposable Subscribe(IObserver<ITaskData> observer) => _observableTask.Subscribe(observer);
}