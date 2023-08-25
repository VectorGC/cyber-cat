using Bonsai;
using Bonsai.Core;
using UnityEngine;

[BonsaiNode("Conditional/Interact/", "IsPlayerPressInteractKey")]
public class IsPlayerPressInteractKey : ConditionalAbort
{
    private Player _player;

    public override void OnEnter()
    {
        _player = FindObjectOfType<Player>();
        base.OnEnter();
    }

    public override bool Condition()
    {
        return _player.IsPressInteract;
    }
}