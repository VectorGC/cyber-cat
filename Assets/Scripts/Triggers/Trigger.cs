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
    [SerializeField] private List<Trigger> _banTriggers;

    [SerializeField] Transform _transformOfTrigger;

    [SerializeField] private bool _canBeActivatedMultipleTime;
    [SerializeField] private int _countOfModals;
    [SerializeField] private ModalInfo[] _modalInfos;
    [SerializeField] private TriggerType _triggerType;
    [SerializeField] private EventType _eventType;
    [SerializeField] private KeyCode[] _keyCodes;

    [SerializeField] private UnityEvent _onEnter;
    [SerializeField] private UnityEvent _onExit;
    [SerializeField] private UnityEvent _onStart;

    private bool _playerInside;
    private bool _buttonPressed;


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
        _onEnter.AddListener(Enter);

        if (_onExit == null)
        {
            _onExit = new UnityEvent();
        }
        _onExit.AddListener(Exit);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!CanBeActivated() || _playerInside || (_triggerType != TriggerType.Enter && _triggerType != TriggerType.EnterAndPress) || !other.TryGetComponent<Player>(out _player))
        {
            return;
        }

        if (!_playerInside)
        {
            _onEnter?.Invoke();
            
        }

        if (_triggerType == TriggerType.Enter)
        {
            Do();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Player>(out _player))
        {
            return;
        }
        _onExit?.Invoke();

        if (_canBeActivatedMultipleTime)
        {
            SetStartDeactivated();
        }
    }

    private void Update()
    {
        if (CanBeActivated())
        {
            foreach (var t in _keyCodes)
            {
                if (Input.GetKeyDown(t))
                {
                    _buttonPressed = true;
                    if (_triggerType == TriggerType.ButtonPressed)
                    {
                        Do();
                    }
                    if (_triggerType == TriggerType.EnterAndPress && _playerInside)
                    {
                        Do();
                    }
                }
            }
        }
    }

    private void Do()
    {
        _onStart?.Invoke();
        SetStartActivated();
        switch (_eventType)
        {
            case EventType.CustomScript:
                _onEnter?.Invoke();
                break;
            case EventType.Message:
                ShowMessage();
                break;
        }
    }

    private void ShowMessage()
    {
        UnityAction[] onUnshow;

        if (_canBeActivatedMultipleTime)
        {
            onUnshow = new UnityAction[]
            {
                SetPlayerActive,
                Activate,
                SetStartDeactivated
            };
        }
        else
        {
            onUnshow = new UnityAction[]
            {
                SetPlayerActive,
                Activate,
            };
        }

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
        if (_triggerType != TriggerType.EnterAndPress)
        {
             _activated = true;
        }

        if (_playerInside && _buttonPressed)
        {
            _buttonPressed = false;
            _activated = true;
        }
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

        for (var index = 0; index < _banTriggers.Count; index++)
        {
            var t = _banTriggers[index];
            var activated = t?.Activated;
            if (activated is true)
            {
                return false;
            }
        }

        return true;
    }

    private void SetPlayerActive()
    {
        Time.timeScale = 1.0f;
    }

    private void SetPlayerInactive()
    {
        Time.timeScale = 0.0f;
    }

    private void SetStartActivated()
    {
        _startActivated = true;
    }

    private void SetStartDeactivated()
    {
        _startActivated = false;
    }

    private void Enter()
    {
        _playerInside = true;
    }

    private void Exit()
    {
        _playerInside = false;
    }

    public enum TriggerType
    {
        Enter = 0,
        ButtonPressed = 1,
        EnterAndPress = 2,
    }

    public enum EventType
    {
        Message = 0,
        CustomScript = 1,
    }
}
