using System.Text;
using Bonsai;
using Bonsai.Core;
using UnityEngine;

[BonsaiNode("Waits/")]
public class WaitToInteractActor : Task
{
    [SerializeField] private string _hint = "Взаимодействовать - F";

    private HUDController _hudController;

    public override void OnEnter()
    {
        _hudController = FindObjectOfType<HUDController>();
    }

    public override Status Run()
    {
        if (!Tree.actor.TryGetComponent<Interactable>(out var actor))
        {
            return Status.Failure;
        }

        var player = Blackboard.Get<Player>("player");

        if (player.InteractPosibility.CanInteract(actor))
        {
            _hudController.HintText = _hint;
            if (player.InteractPosibility.IsPressed)
            {
                return Status.Success;
            }
        }

        return Status.Running;
    }

    public override void Description(StringBuilder builder)
    {
        base.Description(builder);
        builder.AppendLine(_hint);
    }
}