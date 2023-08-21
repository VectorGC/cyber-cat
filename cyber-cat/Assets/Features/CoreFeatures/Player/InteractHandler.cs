using System.Linq;
using UnityEngine;

public class InteractHandler
{
    private readonly Interactable[] _interactables;
    private readonly HUDController _hud;
    private readonly Transform _self;
    private readonly float _interactDistance;

    private Interactable _lastNearestInteractable;

    public InteractHandler(float interactDistance, Interactable[] interactables, HUDController hud, Transform self)
    {
        _interactDistance = interactDistance;
        _self = self;
        _hud = hud;
        _interactables = interactables;
    }

    public void OnUpdate()
    {
        var nearestInteractable = _interactables.FirstOrDefault(IsNearestInteractable);
        if (nearestInteractable == null)
        {
            if (_lastNearestInteractable != null)
            {
                OnExitInteractableZone(_lastNearestInteractable);
                _lastNearestInteractable = null;
            }
        }
        else
        {
            if (nearestInteractable != _lastNearestInteractable)
            {
                OnExitInteractableZone(_lastNearestInteractable);
                OnEnterInteractZone(nearestInteractable);
                _lastNearestInteractable = nearestInteractable;
            }
        }

        if (nearestInteractable != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                OnInteract(nearestInteractable);
            }
        }
    }

    private bool IsNearestInteractable(Interactable interactable)
    {
        var isNearest = Vector3.Distance(_self.position, interactable.transform.position) < _interactDistance;
        return isNearest && interactable.CanInteract;
    }

    private void OnInteract(Interactable interactable)
    {
        interactable.OnInteract();
    }

    private void OnEnterInteractZone(Interactable interactable)
    {
        _hud.HintText = interactable.InteractText;
    }

    private void OnExitInteractableZone(Interactable interactable)
    {
        if (interactable != null)
        {
            interactable.OnExitInteractableZone();
        }

        _hud.HintText = string.Empty;
    }
}