using System;
using System.Collections.Generic;
using System.Linq;
using ApiGateway.Client.Models;
using Cysharp.Threading.Tasks;
using Shared.Models.Ids;
using UniMob;
using UnityEngine;
using Zenject;

public class ConsoleState : ILifetimeScope
{
    [Atom] public IVerdictV2 Verdict { get; set; }
    [Atom] public TestCasesVerdict TestCasesVerdict => Verdict as TestCasesVerdict;
    [Atom] public List<TestCaseId> TestCaseIds => TestCasesVerdict?.Ids.ToList();
    [Atom] public TestCaseId SelectedTestCaseId { get; set; }

    public Lifetime Lifetime { get; }

    public ConsoleState(Lifetime lifetime)
    {
        Lifetime = lifetime;
    }
}

public class ConsoleContentView : LifetimeMonoBehaviour
{
    [SerializeField] private TestCasesView _testCasesView;

    private ICodeEditor _codeEditor;

    [Inject]
    private void Construct(ICodeEditor codeEditor)
    {
        _codeEditor = codeEditor;
    }

    protected override async void Start()
    {
        var state = new ConsoleState(Lifetime);

        _testCasesView.State = state;

        await UniTask.WaitUntil(() => _codeEditor.Task != null);
        state.Verdict = await _codeEditor.Task.VerifySolutionV2("Test");
    }
}