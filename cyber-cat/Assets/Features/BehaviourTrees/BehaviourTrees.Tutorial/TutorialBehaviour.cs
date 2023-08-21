using JetBrains.Annotations;
using Panda;
using UnityEngine;

public class TutorialBehaviour : BaseBehaviourTreeController
{
    [SerializeField] private GameObject _player;
    [SerializeField] private TaskKeeper _taskKeeper;

    [Task]
    [UsedImplicitly]
    public bool IsDistance_LessThan(int closestDistance)
    {
        var distance = Vector3.Distance(_player.transform.position, _taskKeeper.transform.position);
        ThisTask.debugInfo = $"{distance} < {closestDistance}";
        return distance < closestDistance;
    }
}