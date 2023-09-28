using System;
using UniMob;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ConsoleToolSectionToggle : LifetimeUIBehaviour<CodeEditorState>
{
    private enum SectionType
    {
        TestCases,
        Result
    }

    [field: SerializeField] public Toggle Toggle { get; private set; }
    [SerializeField] private SectionType _type;

    [Atom] public override CodeEditorState State { get; set; }

    private ICodeEditor _codeEditor;

    [Inject]
    private void Construct(ICodeEditor codeEditor)
    {
        _codeEditor = codeEditor;
    }

    protected override void OnInitView()
    {
        Toggle.onValueChanged.AddListener(OnValueChanged);
    }

    protected override void OnDisposeView()
    {
        Toggle.onValueChanged.RemoveListener(OnValueChanged);
    }

    protected override void OnInitState(CodeEditorState state)
    {
        if (Toggle.isOn)
            OnValueChanged(true);
    }

    private void OnValueChanged(bool value)
    {
        if (value == false || State == null)
            return;


        switch (_type)
        {
            case SectionType.TestCases:
                State.Section = new TestCasesSection(Lifetime)
                {
                    SelectedTestCaseId = State.GetSelectedTestCaseId()
                };
                break;
            case SectionType.Result:
                State.Section = new ResultSection(Lifetime)
                {
                    SelectedTestCaseId = State.GetSelectedTestCaseId()
                };
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        State.CallSectionChanged();
    }
}