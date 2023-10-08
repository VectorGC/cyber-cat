using System;
using ApiGateway.Client.Internal.Tasks.Statuses;
using ApiGateway.Client.Internal.Tasks.Verdicts;
using Cysharp.Threading.Tasks;
using Models;
using Shared.Models.Models.TestCases;
using Shared.Models.Models.Verdicts;
using UniMob;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CodeEditorController : LifetimeUIBehaviour<CodeEditorState>
{
    [SerializeField] private SerializableInterface<IText> _taskDescription;
    [SerializeField] private CodeEditorView _codeEditorView;
    [SerializeField] private CodeConsoleController _console;
    [SerializeField] private CodeEditorStatusBar _statusBar;
    [Header("Buttons")] [SerializeField] private Button _verifySolution;
    [SerializeField] private Button _loadSavedCode;
    [SerializeField] private Button _resetCode;
    [SerializeField] private IButtonInterface _exit;

    [Header("Debug")] [SerializeField] private TaskType _taskTypeDebug;

    [Atom] public override CodeEditorState State { get; set; }

    private ICodeEditor _codeEditor;
    private TestCases _testCasesCache;
    private Verdict _verdictCache;

    [Inject]
    private async void Construct(ICodeEditor codeEditor)
    {
        _codeEditor = codeEditor;

#if UNITY_EDITOR
        if (Application.isEditor && _codeEditor.Task == null)
        {
            var typedEditor = (CodeEditor) codeEditor;
            await typedEditor.LoadDebugTaskCheat(_taskTypeDebug);
        }
#endif

        var descriptionHandler = codeEditor.Task.GetDescription();
        _taskDescription.Value.SetTextAsync(descriptionHandler);
        _codeEditorView.Language = LanguageProg.Cpp;
    }

    protected override async void Start()
    {
        base.Start();

        await UniTask.WaitUntil(() => _codeEditor.Task != null);

        var progress = await _codeEditor.Task.GetStatus();
        switch (progress)
        {
            case NotStarted notStarted:
                _codeEditorView.SourceCode = await _codeEditor.Task.GetDefaultCode();
                break;
            case Complete complete:
                _codeEditorView.SourceCode = complete.Solution;
                break;
            case HaveSolution haveSolution:
                _codeEditorView.SourceCode = haveSolution.Solution;
                break;
        }

        _testCasesCache = await _codeEditor.Task.GetTestCases();
        State = new CodeEditorState(Lifetime)
        {
            Section = new TestCasesSection(Lifetime)
            {
                TestCases = _testCasesCache
            }
        };
    }

    private void OnSectionChanged()
    {
        OnSectionChangedAsync().Forget();
    }

    private async UniTaskVoid OnSectionChangedAsync()
    {
        switch (State.Section)
        {
            case ResultSection resultSection:
                if (_verdictCache != null)
                    resultSection.Verdict = _verdictCache;
                break;
            case TestCasesSection testCasesSection:
                _testCasesCache ??= await _codeEditor.Task.GetTestCases();
                testCasesSection.TestCases = _testCasesCache;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected override void OnInitView()
    {
        _verifySolution.onClick.AddListener(VerifySolution);
        _loadSavedCode.onClick.AddListener(GetSavedCode);
        _resetCode.onClick.AddListener(ResetCode);
        _exit.Clicked += ExitEditor;
    }

    protected override void OnDisposeView()
    {
        _verifySolution.onClick.AddListener(VerifySolution);
        _loadSavedCode.onClick.RemoveListener(GetSavedCode);
        _resetCode.onClick.RemoveListener(ResetCode);
        _exit.Clicked -= ExitEditor;
    }

    protected override void OnInitState(CodeEditorState state)
    {
        _console.State = State;
        _statusBar.State = State;

        State.SectionChanged += OnSectionChanged;
    }

    protected override void OnDisposeState(CodeEditorState state)
    {
        State.SectionChanged -= OnSectionChanged;
    }

    protected override void OnUpdate()
    {
        if (_verdictCache is Success)
        {
            _exit.SetActiveHighlight(true);
        }
        else
        {
            _exit.SetActiveHighlight(false);
        }
    }

    private async void VerifySolution()
    {
        var sourceCode = _codeEditorView.SourceCode;
        _verdictCache = await _codeEditor.Task.VerifySolution(sourceCode).ToReportProgressStatus(State, "Выполняется...");

        // Force select Result section.
        State.Section = new ResultSection(Lifetime)
        {
            Verdict = _verdictCache
        };
    }

    private async void GetSavedCode()
    {
        var sourceCode = await _codeEditor.Task.GetLastSavedSolution();
        _codeEditorView.SourceCode = sourceCode;
    }

    private async void ResetCode()
    {
        var sourceCode = await _codeEditor.Task.GetDefaultCode();
        _codeEditorView.SourceCode = sourceCode;
    }

    private void ExitEditor()
    {
        _codeEditor.Close();
    }
}