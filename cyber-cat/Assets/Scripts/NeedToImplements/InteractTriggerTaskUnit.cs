using System;
using Legacy_do_not_use_it;
using TaskUnits;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractTriggerTaskUnit : MonoBehaviourObserver<ITaskData>
{
    [SerializeField] private TasksInfo.TaskTypes _task;
    [SerializeField] private Trigger _triggerToActivate;
    private ITaskData _taskData;

    [SerializeField] private bool selfTriggerLogic = false;

    protected void Start()
    {
        var collider = GetComponent<Collider>();
        if (_taskData == null)
        {
            collider.enabled = false;
        }
    }

    private bool _trigger;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        _trigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        _trigger = false;
    }

    private void Update()
    {
        if (!_trigger)
        {
            return;
        }

        if (!selfTriggerLogic)
        {
            return;
        }

        var isHackModePressed = Input.GetKeyDown(KeyCode.F);
        if (isHackModePressed && GameMode.Vision == VisionMode.HackVision)
        {
            var progress = new ScheduledNotifier<float>();
            progress.ViaLoadingScreen();

            new CodeEditorService().OpenEditor(_task.GetId(), progress).Forget();
        }
    }

    public void Load()
    {
        var progress = new ScheduledNotifier<float>();
        progress.ViaLoadingScreen();

        new CodeEditorService().OpenEditor(_task.GetId(), progress).Forget();
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

            var collider = GetComponent<Collider>();
            collider.enabled = true;
        }
    }

    public override void OnCompleted()
    {
        if (_triggerToActivate)
        {
            _triggerToActivate.gameObject.SetActive(true);
        }

        var collider = GetComponent<Collider>();
        collider.enabled = false;
    }

    public override void OnError(Exception error)
    {
        var collider = GetComponent<Collider>();
        collider.enabled = false;

        if (selfTriggerLogic)
        {
            gameObject.SetActive(false);
        }
    }
}