using System;
using System.Linq;
using Shared.Models.Ids;
using UnityEngine.UI;
using Zenject;

public class TestCasesToggleGroup : ToggleGroup
{
    public event EventHandler<TestCaseId> Switched;

    private ICodeEditor _codeEditor;
    private TestCaseToggle[] _toggles;

    [Inject]
    private void Construct(ICodeEditor codeEditor)
    {
        _codeEditor = codeEditor;
    }

    protected override async void Start()
    {
        _toggles = GetComponentsInChildren<TestCaseToggle>();
        foreach (var toggle in _toggles)
        {
            toggle.Enabled += OnEnabled;
        }

        var testCasesDict = await _codeEditor.Task.GetTestCases();
        var testCases = testCasesDict.Keys.ToArray();
        for (var i = 0; i < _toggles.Length; i++)
        {
            if (i < testCases.Length)
            {
                _toggles[i].TestCaseId = testCases[i];
                _toggles[i].Enabled += OnEnabled;
                RegisterToggle(_toggles[i].Toggle);
                _toggles[i].Toggle.group = this;
            }
            else
            {
                _toggles[i].gameObject.SetActive(false);
            }
        }

        base.Start();
    }

    protected override void OnDestroy()
    {
        foreach (var toggle in _toggles)
        {
            toggle.Enabled -= OnEnabled;
            UnregisterToggle(toggle.Toggle);
        }

        base.OnDestroy();
    }

    private void OnEnabled(object sender, TestCaseId id)
    {
        Switched?.Invoke(sender, id);
    }
}