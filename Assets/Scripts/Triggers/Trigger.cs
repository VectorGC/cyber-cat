using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
[RequireComponent(typeof(SphereCollider))]
public class Trigger : MonoBehaviour
{
    private Player _player;
    private ModalPanel _modalPanel;
    private bool _activated;

    public Player Player => _player;
    public ModalPanel ModalPanel => _modalPanel;

    [SerializeField] Transform _transformOfTrigger;
    [SerializeField] ModalInfo _modalInfo;

    [SerializeField] private int _countOfModals;
    [SerializeField] ModalInfo[] _modalInfos;

    [SerializeField] private UnityEvent _onEnter;


    private void Start()
    {
        Init();
    }

    public void Init()
    {
        if (_transformOfTrigger != null)
        {
            transform.position = _transformOfTrigger.position;
        }
        
        _activated = false;
        _player = GameObject.FindObjectOfType<Player>();
        _modalPanel = ModalPanel.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_activated && other.TryGetComponent<Player>(out _player))
        {
            _activated = true;
            _onEnter?.Invoke();
            _modalPanel.MessageBos(_modalInfos);
        }
    }
}
