using JetBrains.Annotations;
using Panda;
using UnityEngine;

public class TaskQuestBehaviour : BaseBehaviourTreeController
{
    [SerializeField] private TaskKeeper _taskKeeper;

    [Task]
    [UsedImplicitly]
    public void ActivateTask()
    {
    }
    
    [Task]
    [UsedImplicitly]
    public void Wait_InteractToTask()
    {
        
    }

    [Task]
    [UsedImplicitly]
    public void True()
    {
    }
}