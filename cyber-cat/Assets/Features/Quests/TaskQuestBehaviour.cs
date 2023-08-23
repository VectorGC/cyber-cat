using JetBrains.Annotations;
using Panda;
using UnityEngine;

public class TaskQuestBehaviour : BaseBehaviourTreeController
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _taskKeeper;

    [Task]
    [UsedImplicitly]
    public void ActivateTask()
    {
    }

    [Task]
    [UsedImplicitly]
    public bool IfPlayerCanInteractToTask()
    {
        var distance = Vector3.Distance(_player.transform.position, _taskKeeper.transform.position);
        const float interactDistance = Player.InteractDistance;
        ThisTask.debugInfo = $"{distance} < {interactDistance}";

        return distance < interactDistance;
    }

    [Task]
    [UsedImplicitly]
    public void OpenCodeEditor()
    {
        CodeEditor.Open(TaskType.Task1.Id());
        if (!CodeEditor.IsOpen)
        {
            ThisTask.Succeed();
        }
    }
    
    [Task]
    [UsedImplicitly]
    public bool IfTaskSolved()
    {
        return true;
    }
}