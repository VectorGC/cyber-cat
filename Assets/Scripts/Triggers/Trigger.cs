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
    [SerializeField] private bool _activated;
    [SerializeField] private bool _startActivated;

    public Player Player => _player;
    public ModalPanel ModalPanel => _modalPanel;
    public bool Activated => _activated;

    [SerializeField] private List<Trigger> _requiredTriggers;

    [SerializeField] Transform _transformOfTrigger;
    [SerializeField] ModalInfo _modalInfo;

    [SerializeField] private int _countOfModals;
    [SerializeField] private ModalInfo[] _modalInfos;
    [SerializeField] private TriggerType _triggerType;
    [SerializeField] private EventType _eventType;
    [SerializeField] private KeyCode[] _keyCodes;

    private UnityEvent _entered;
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
        _startActivated = false;
        _player = GameObject.FindObjectOfType<Player>();
        _modalPanel = FindObjectOfType<ModalPanel>();

        GetComponent<SphereCollider>().isTrigger = true;
        if (_onEnter == null)
        {
            _onEnter = new UnityEvent();
        }
        _onEnter.AddListener(Activate);
        
        //_entered.AddListener(Activate);
    }

    private void OnTriggerStay(Collider other)
    {
        if (_startActivated || _triggerType != TriggerType.Enter || !other.TryGetComponent<Player>(out _player))
        {
            return;
        }
        _entered?.Invoke();

        Do();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Player>(out _player))
        {
            return;
        }
    }

    private void Update()
    {
        if (!_startActivated)
        {
            foreach (var t in _keyCodes)
            {
                if (Input.GetKeyDown(t))
                {
                    Do();
                }
            }
        }
    }

    private void Do()
    {
        if (!CanBeActivated())
        {
            return;
        }
        SetStartActivated();
        switch (_eventType)
        {
            case EventType.EnterEvent:
                _onEnter?.Invoke();
                break;
            case EventType.Message:
                ShowMessage();
                break;
        }
    }

    private void ShowMessage()
    {
        UnityAction[] onUnshow = new UnityAction[]
        {
            SetPlayerActive,
            Activate,
        };

        UnityAction[] onShow = new UnityAction[]
        {
            SetPlayerInactive,
        };

        if (_modalPanel)
        {
            _modalPanel.MessageBos(_modalInfos, onUnshow, onShow);
        }
    }

    private void Activate()
    {
        _activated = true;
        //_player.gameObject.SetActive(false);
    }

    private bool CanBeActivated()
    {
        if (_startActivated)
        {
            return false;
        }
        for (var index = 0; index < _requiredTriggers.Count; index++)
        {
            var t = _requiredTriggers[index];
            if (!t.Activated)
            {
                return false;
            }
        }

        return true;
    }

    private void SetPlayerActive()
    {
        //_player.gameObject.SetActive(true);
        Time.timeScale = 1.0f;
    }

    private void SetPlayerInactive()
    {
        Time.timeScale = 0.0f;
        //_player.gameObject.SetActive(false);
    }

    private void SetStartActivated()
    {
        _startActivated = true;
    }

    private void SetStartDeactivated()
    {
        _startActivated = false;
    }

    public enum TriggerType
    {
        Enter = 0,
        ButtonPressed = 1
    }

    public enum EventType
    {
        Message = 0,
        EnterEvent = 1,
    }
}
