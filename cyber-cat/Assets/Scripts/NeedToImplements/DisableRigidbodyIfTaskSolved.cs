using System;
using TaskUnits;
using UnityEngine;

public class DisableRigidbodyIfTaskSolved : MonoBehaviourObserver<ITaskData>
{
    [SerializeField] private Rigidbody RigidbodyToDisable;
    [SerializeField] private Collider ActiveRidigbodyColliderOnDisable;

    private void Start()
    {
        ActiveRidigbodyColliderOnDisable.enabled = false;
    }

    public override void OnError(Exception error)
    {
    }

    public override void OnNext(ITaskData value)
    {
    }

    public override void OnCompleted()
    {
        RigidbodyToDisable.useGravity = true;
        ActiveRidigbodyColliderOnDisable.enabled = true;
    }
}