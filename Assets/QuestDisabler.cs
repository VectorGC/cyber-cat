using UnityEngine;
using System;
using TasksData;

public class QuestDisabler : MonoBehaviourObserver<ITaskData>
{
    [SerializeField] private Trigger _triggerToDisable;

    public override void OnCompleted()
    {
        _triggerToDisable.enabled = false;
        if (_triggerToDisable.gameObject.TryGetComponent(out SphereCollider collider))
        {
            collider.radius = 0;
        }
    }

    public override void OnError(Exception error) => OnCompleted();

    public override void OnNext(ITaskData value)
    {
    }
}