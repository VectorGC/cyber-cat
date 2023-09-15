using UnityEngine;

public class InteractPosibilityHandler
{
    public bool IsPressed { get; private set; }

    private const float _interactDistance = 2;

    private readonly Interactable[] _interactables;
    private readonly HUDController _hud;
    private readonly Transform _self;

    private float _pressTimer;

    public InteractPosibilityHandler(Interactable[] interactables, HUDController hud, Transform self)
    {
        _self = self;
        _hud = hud;
        _interactables = interactables;
    }

    public void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            IsPressed = true;
            _pressTimer = 1;
        }

        if (_pressTimer > 0)
            _pressTimer -= Time.deltaTime;
        else
            IsPressed = false;
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