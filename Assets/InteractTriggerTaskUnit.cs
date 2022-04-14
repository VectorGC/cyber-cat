using System;
using Legacy_do_not_use_it;
using TasksData;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractTriggerTaskUnit : MonoBehaviourObserver<ITaskData>
{
    [SerializeField] private Trigger _triggerToActivate;
    private ITaskData _taskData;
    private Collider _collider;
    
    [SerializeField] private bool selfTriggerLogic = false;

    protected void Start()
    {
        TryGetComponent(out _collider);
        _collider.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!selfTriggerLogic)
        {
            return;
        }
        
        if (!other.CompareTag("Player"))
        {
            return;
        }

        var isHackModePressed = Input.GetKey(KeyCode.F);
        if (isHackModePressed && GameMode.Vision == VisionMode.HackVision)
        {
            var progress = new ScheduledNotifier<float>();
            progress.ViaLoadingScreen();

            CodeEditor.OpenSolution(_taskData, progress).Forget();
        }
    }

    public void Load()
    {
        var progress = new ScheduledNotifier<float>();
        progress.ViaLoadingScreen();

        CodeEditor.OpenSolution(_taskData, progress).Forget();
    }


    public override void OnNext(ITaskData taskData)
    {
        _taskData = taskData;
        
        var isTaskSolved = taskData.IsSolved;
        if (isTaskSolved is false)
        {
            if (selfTriggerLogic)
            {
                gameObject.SetActive(true);
            }
            
            _collider.enabled = true;
            return;
        }

        if (_triggerToActivate)
        {
            _triggerToActivate.gameObject.SetActive(true);
        }

        _collider.enabled = false;

        if (selfTriggerLogic)
        {
            gameObject.SetActive(false);
        }
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