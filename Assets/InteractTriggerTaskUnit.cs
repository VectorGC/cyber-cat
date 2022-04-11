using System;
using Legacy_do_not_use_it;
using TasksData;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractTriggerTaskUnit : MonoBehaviourObserver<ITaskData>
{
    private ITaskData _taskData;
    private Collider _collider;

    protected void Start()
    {
        TryGetComponent(out _collider);
        _collider.enabled = false;
    }

    //private async void OnTriggerStay(Collider other)
    //{
    //    if (!other.CompareTag("Player"))
    //    {
    //        return;
    //    }

    //    var isHackModePressed = Input.GetKey(KeyCode.F);
    //    if (isHackModePressed && GameMode.Vision == VisionMode.HackVision)
    //    {
    //        var progress = new ScheduledNotifier<float>();
    //        progress.ViaLoadingScreen();

    //        await CodeEditor.OpenSolution(_taskData, progress);
    //    }
    //}

    public async void Load()
    {
        var progress = new ScheduledNotifier<float>();
        progress.ViaLoadingScreen();

        await CodeEditor.OpenSolution(_taskData, progress);
    }


    public override void OnNext(ITaskData taskData)
    {
        _taskData = taskData;
        
        var isTaskSolved = taskData?.IsSolved;
        if (isTaskSolved is false)
        {
            _collider.enabled = true;
            return;
        }

        _collider.enabled = false;
    }

    public override void OnCompleted()
    {
        Destroy(this);
    }

    public override void OnError(Exception error)
    {
        throw error;
    }
}