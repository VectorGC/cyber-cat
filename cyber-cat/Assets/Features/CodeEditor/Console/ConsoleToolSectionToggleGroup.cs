using UniMob;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleToolSectionToggleGroup : LifetimeUIBehaviour<CodeEditorState>
{
    [SerializeField] private ToggleGroup _toggleGroup;

    [Atom] public override CodeEditorState State { get; set; }

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

    protected override void OnInitState(CodeEditorState state)
    {
        foreach (var toggle in _toggles)
        {
            toggle.State = state;
        }
    }

    protected override void OnUpdate()
    {
        switch (State?.Section)
        {
            case TestCasesSection testCasesSection:
                _toggles[0].Toggle.SetIsOnWithoutNotify(true);
                break;
            case ResultSection resultSection:
                _toggles[1].Toggle.SetIsOnWithoutNotify(true);
                break;
        }
    }
}