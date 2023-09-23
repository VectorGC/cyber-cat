using System;
using ApiGateway.Client.Models;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class ConsoleContentView : UIBehaviour
{
    [SerializeField] private SerializableInterface<ITestCasesView> _testCasesView;

    private ICodeEditor _codeEditor;

    [Inject]
    private void Construct(ICodeEditor codeEditor)
    {
        _codeEditor = codeEditor;
    }

    protected override async void Start()
    {
        var verdict = await _codeEditor.Task.VerifySolutionV2("Test");
        switch (verdict)
        {
            case CompileError compileError:
                break;
            case TestCasesVerdict testCasesVerdict:
                _testCasesView.Value.Show(testCasesVerdict);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(verdict));
        }
    }

    protected override void OnDestroy()
    {
        _testCasesView.Value.Hide();
    }
}