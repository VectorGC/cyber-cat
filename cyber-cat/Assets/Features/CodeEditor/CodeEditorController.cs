using System;
using System.Collections.Generic;
using ApiGateway.Client.Application;
using Cysharp.Threading.Tasks;
using Models;
using Shared.Models.Domain.TestCase;
using Shared.Models.Domain.Verdicts;
using Shared.Models.Domain.Verdicts.TestCases;
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
    private List<TestCaseDescription> _testCasesCache;
    private Verdict _verdictCache;
    private ApiGatewayClient _client;

    [Inject]
    private async void Construct(ICodeEditor codeEditor, ApiGatewayClient client)
    {
        _client = client;
        _codeEditor = codeEditor;

#if UNITY_EDITOR
        if (Application.isEditor && _codeEditor.Task == null)
        {
            var typedEditor = (CodeEditor) codeEditor;
            await typedEditor.LoadDebugTaskCheat(_taskTypeDebug);
        }
#endif

        _taskDescription.Value.SetText(codeEditor.Task.Description);
        _codeEditorView.Language = LanguageProg.Cpp;
    }

    protected override async void Start()
    {
        base.Start();

        await UniTask.WaitUntil(() => _codeEditor.Task != null);

        if (_client.Player != null)
        {
            var taskModel = _client.Player.Tasks[_codeEditor.Task.Id];
            if (!taskModel.IsStarted)
                _codeEditorView.SourceCode = taskModel.Description.DefaultCode;
            else
                _codeEditorView.SourceCode = taskModel.LastSolution;
        }

        _testCasesCache = await _client.TaskRepository.GetTestCases(_codeEditor.Task.Id);
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
                _testCasesCache ??= await _client.TaskRepository.GetTestCases(_codeEditor.Task.Id);
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
        _exit.Click += ExitEditor;
    }

    protected override void OnDisposeView()
    {
        _verifySolution.onClick.AddListener(VerifySolution);
        _loadSavedCode.onClick.RemoveListener(GetSavedCode);
        _resetCode.onClick.RemoveListener(ResetCode);
        _exit.Click -= ExitEditor;
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
        if (_verdictCache?.IsSuccess ?? false)
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
        var result = _client.Player != null
            ? await _client.Player.Tasks[_codeEditor.Task.Id].SubmitSolution(sourceCode).ToReportProgressStatus(State, "Выполняется...")
            : await _client.JudgeService.GetVerdict(_codeEditor.Task.Id, sourceCode).ToReportProgressStatus(State, "Выполняется...");
        if (result.IsSuccess)
        {
            _verdictCache = result.Value;
        }
        else
        {
            Debug.LogError(result.Error);
            _verdictCache = new NativeFailure()
            {
                Error = result.Error,
                Solution = sourceCode,
                TaskId = _codeEditor.Task.Id,
                TestCases = new TestCasesVerdict()
            };
        }

        // Force select Result section.
        State.Section = new ResultSection(Lifetime)
        {
            Verdict = _verdictCache
        };
    }

    private void GetSavedCode()
    {
        if (_client.Player != null)
        {
            var taskModel = _client.Player.Tasks[_codeEditor.Task.Id];
            _codeEditorView.SourceCode = taskModel.LastSolution;
        }
        else
        {
            _codeEditorView.SourceCode = _codeEditor.Task.DefaultCode;
        }
    }

    private void ResetCode()
    {
        var sourceCode = _codeEditor.Task.DefaultCode;
        _codeEditorView.SourceCode = sourceCode;
    }

    private void ExitEditor()
    {
        _codeEditor.Close();
    }
}