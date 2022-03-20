using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public abstract class Trigger : MonoBehaviour
{
    
    private Player _player;
    private ModalPanel _modalPanel;
    private bool _activated;
    private UnityEvent _onEnter;

    public Player Player => _player;
    public ModalPanel ModalPanel => _modalPanel;
    

    public void Init()
    {
        _activated = false;
        _player = GameObject.FindObjectOfType<Player>();
        _modalPanel = ModalPanel.Instance;
        if (_onEnter == null)
        {
            _onEnter = new UnityEvent();
        }
        _onEnter.RemoveAllListeners();
        _onEnter.AddListener(Enter);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_activated && other.TryGetComponent<Player>(out _player))
        {
            _activated = true;
            _onEnter?.Invoke();
        }
    }

    public abstract void Enter();
}
