using UniMob;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleToolSectionToggle : LifetimeUIBehaviour<ConsoleState>
{
    private int _index;
    [field: SerializeField] public Toggle Toggle { get; private set; }
    
    [Atom] public override ConsoleState State { get; set; }

    protected override void OnInitView()
    {
        Toggle.onValueChanged.AddListener(OnValueChanged);
    }

    protected override void OnDisposeView()
    {
        Toggle.onValueChanged.RemoveListener(OnValueChanged);
    }

    public void SetIndexNumber(int index)
    {
        _index = index;
    }
    
    private void OnValueChanged(bool value)
    {
    }
}

public class ConsoleToolSectionToggleGroup : LifetimeUIBehaviour<ConsoleState>
{
    [SerializeField] private ToggleGroup _toggleGroup;

    [Atom] public override ConsoleState State { get; set; }

    private ConsoleToolSectionToggle[] _toggles;

    protected override void OnInitView()
    {
        _toggles = GetComponentsInChildren<ConsoleToolSectionToggle>();
        for (var i = 0; i < _toggles.Length; i++)
        {
            var toggle = _toggles[i];
            _toggleGroup.RegisterToggle(toggle.Toggle);
            toggle.Toggle.group = _toggleGroup;
            toggle.SetIndexNumber(i);
        }
    }

    protected override void OnDisposeView()
    {
        foreach (var toggle in _toggles)
        {
            _toggleGroup.UnregisterToggle(toggle.Toggle);
            toggle.Toggle.group = null;
        }
    }
}