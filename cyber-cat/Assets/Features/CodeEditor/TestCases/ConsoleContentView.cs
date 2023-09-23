using System;
using ApiGateway.Client.Models;
using UniMob;
using UnityEngine;
using Zenject;

public class ConsoleContentView : LifetimeUIBehaviour
{
    [SerializeField] private SerializableInterface<ITestCasesView> _testCasesView;

    private ICodeEditor _codeEditor;

    [Atom] public IVerdictV2 Verdict { get; private set; }

    [Inject]
    private void Construct(ICodeEditor codeEditor)
    {
        _codeEditor = codeEditor;
    }

    protected override async void Start()
    {
        base.Start();
        Verdict = await _codeEditor.Task.VerifySolutionV2("Test");
    }

    protected override void OnUpdate()
    {
        if (Verdict != null)
        {
            switch (Verdict)
            {
                case CompileError compileError:
                    break;
                case TestCasesVerdict testCasesVerdict:
                    _testCasesView.Value.TestCasesVerdict = testCasesVerdict;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Verdict));
            }
        }
    }
}