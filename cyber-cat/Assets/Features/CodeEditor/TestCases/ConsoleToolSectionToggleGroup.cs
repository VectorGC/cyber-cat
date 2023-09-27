using UniMob;
using UnityEngine;
using UnityEngine.UI;

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

    protected override void OnInitState(ConsoleState state)
    {
        foreach (var toggle in _toggles)
        {
            toggle.State = state;
        }
    }
}