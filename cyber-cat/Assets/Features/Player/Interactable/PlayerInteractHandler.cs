using System.Linq;
using UnityEngine;

public class PlayerInteractHandler
{
    private const float _interactDistance = 2;

    private readonly Interactable[] _interactables;
    private readonly IHud _hud;
    private readonly Transform _self;

    public PlayerInteractHandler(Interactable[] interactables, IHud hud, Player player)
    {
        _self = player.transform;
        _hud = hud;
        _interactables = interactables;
    }

    public void OnUpdate()
    {
        Interactable interctableTarget = null;
        foreach (var interactable in _interactables)
        {
            if (CanInteract(interactable))
            {
                interctableTarget = interactable;
                break;
            }
        }

        if (interctableTarget != null)
        {
            _hud.HintText = "Взаимодействовать F";
            if (Input.GetKeyDown(KeyCode.F))
            {
                interctableTarget.Interact();
            }
        }
    }

    public bool CanInteract(Interactable interactable)
    {
        if (!interactable.CanInteract)
        {
            return false;
        }

        return Vector3.Distance(_self.position, interactable.transform.position) <= _interactDistance;
    }
}