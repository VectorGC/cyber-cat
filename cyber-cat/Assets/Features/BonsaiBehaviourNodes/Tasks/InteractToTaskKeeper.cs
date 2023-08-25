using System.Collections;
using Bonsai;
using Bonsai.Core;
using UnityEngine;

[BonsaiNode("Tasks/Interact/", "InteractToTaskKeeper")]
public class InteractToTaskKeeper : Task
{
    [SerializeField] public TaskType _task;

    private TaskKeeper _keeper;
    private Player _player;
    private Status _onEnterStatus;
    private IEnumerator _coroutine;

    public override void OnEnter()
    {
        _keeper = TaskKeeper.FindKeeperForTask(_task);
        _player = FindObjectOfType<Player>();

        _onEnterStatus = _player.CanInteract(_keeper) ? Status.Success : Status.Failure;
        if (_onEnterStatus == Status.Success)
        {
            _coroutine = _keeper.Interact();
        }
    }

    public override Status Run()
    {
        if (_onEnterStatus == Status.Failure)
        {
            return Status.Failure;
        }

        return _coroutine.MoveNext() ? Status.Running : Status.Success;
    }
}