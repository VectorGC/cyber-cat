using System;
using System.Collections.Generic;
using Shared.Models.Ids;
using UnityEngine.UI;

public class TestCasesToggleGroup : ToggleGroup
{
    public event EventHandler<TestCaseId> Switched;

    private TestCaseToggle[] _toggles;

    protected override void Awake()
    {
        _toggles = GetComponentsInChildren<TestCaseToggle>();
        foreach (var toggle in _toggles)
        {
            RegisterToggle(toggle.Toggle);
            toggle.Toggle.group = this;
            toggle.Enabled += OnEnabled;
        }
    }

    protected override void OnDestroy()
    {
        foreach (var toggle in _toggles)
        {
            UnregisterToggle(toggle.Toggle);
            toggle.Toggle.group = null;
            toggle.Enabled -= OnEnabled;
        }
    }

    public void Show(List<TestCaseId> testCaseIds)
    {
        var i = 0;
        for (; i < testCaseIds.Count; i++)
        {
            _toggles[i].TestCaseId = testCaseIds[i];
        }

        for (; i < _toggles.Length; i++)
        {
            _toggles[i].gameObject.SetActive(false);
        }

        EnsureValidState();
    }

    public void Hide()
    {
    }

    private void OnEnabled(object sender, TestCaseId id)
    {
        Switched?.Invoke(sender, id);
    }
}