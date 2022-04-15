using System;
using TasksData;
using UnityEngine;

public class DisableRigidbodyIfTaskSolved : MonoBehaviourObserver<ITaskData>
{
    [SerializeField] private Rigidbody RigidbodyToDisable;
    [SerializeField] private Hover HoverComponent;
    [SerializeField] private Rotate RotateComponent;

    public override void OnError(Exception error)
    {
    }

    public override void OnNext(ITaskData value)
    {
    }
    
    public override void OnCompleted()
    {
        RigidbodyToDisable.useGravity = true;
        HoverComponent.enabled = false;
        RotateComponent.enabled = false;
    }
}
