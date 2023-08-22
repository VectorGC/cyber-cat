using Panda;
using UnityEngine;

[RequireComponent(typeof(BaseBehaviourTreeController))]
public class OnInteractBehaviourTree : Interactable
{
    private PandaBehaviour _pandaBehaviour;

    public void Start()
    {
        TryGetComponent(out _pandaBehaviour);
        _pandaBehaviour.enabled = false;
    }

    public override bool CanInteract => _pandaBehaviour.status == Status.Ready || _pandaBehaviour.status == Status.Running;

    public override void OnInteract()
    {
        _pandaBehaviour.enabled = true;
    }

    public override void OnExitInteractableZone()
    {
        _pandaBehaviour.enabled = false;
    }
}