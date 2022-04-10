using System;
using TasksData;
using UnityEngine;

public abstract class TaskUnitView: MonoBehaviour, IObserver<ITaskData>
{
    [SerializeField] protected TaskUnitController taskUnit;

    protected bool IsTaskSolved { get; private set; }

    protected virtual async void Start()
    {
        var taskData = await taskUnit.GetTask();
        OnNext(taskData);
        
        taskUnit.Subscribe(this);
    }

    protected abstract void UpdateTaskData(ITaskData taskData);

    public void OnNext(ITaskData taskData)
    {
        IsTaskSolved = taskData is {IsSolved: true};
        UpdateTaskData(taskData);
    }
    
    public void OnCompleted()
    {
    }

    public void OnError(Exception error)
    {
    }
}