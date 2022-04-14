using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TasksData;

public class QuestDisabler : MonoBehaviourObserver<ITaskData>
{
    [SerializeField] private Trigger _triggerToDisable;

    public override void OnCompleted()
    {
        Destroy(this);
    }

    public override void OnError(Exception error)
    {
        throw error;
    }

    public override void OnNext(ITaskData value)
    {
        if (value.IsSolved is true)
        {
            _triggerToDisable.enabled = false;
            if (_triggerToDisable.gameObject.TryGetComponent<SphereCollider>(out SphereCollider collider))
            {
                collider.radius = 0;
            }
        }
        
    }
}
