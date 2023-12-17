using System;
using System.Collections.Generic;
using System.Diagnostics;
using ApiGateway.Client.Application;
using ApiGateway.Client.Application.CQRS;
using Cysharp.Threading.Tasks;
using Models;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.TestCase;
using Shared.Models.Domain.Verdicts;
using Shared.Models.Domain.Verdicts.TestCases;
using UniMob;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Debug = UnityEngine.Debug;

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

    private static bool IsSuccessSubmitCheat
    {
        get => EditorPrefs.GetBool(nameof(IsSuccessSubmitCheat));
        set => EditorPrefs.SetBool(nameof(IsSuccessSubmitCheat), value);
    }

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

    [MenuItem("Cheats/Always success submit")]
    private static void SuccessSubmitCheat()
    {
        IsSuccessSubmitCheat = !IsSuccessSubmitCheat;
    }

    [MenuItem("Cheats/Always success submit", true)]
    private static bool SuccessSubmitCheatValidate()
    {
        Menu.SetChecked("Cheats/Always success submit", IsSuccessSubmitCheat);
        return true;
    }

    [MenuItem("Cheats/ClearPlayerPrefs")]
    private static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    protected override async void Start()
    {
        base.Start();

        await UniTask.WaitUntil(() => _codeEditor.Task != null);

        if (_client.Player == null)
        {
            var verdict = _client.VerdictHistoryService.GetBestOrLastVerdict(_codeEditor.Task.Id);
            if (verdict != null)
            {
                _codeEditorView.SourceCode = verdict.Solution;
            }
            else
            {
                var description = await _client.TaskRepository.GetTaskDescription(_codeEditor.Task.Id);
                _codeEditorView.SourceCode = description.DefaultCode;
            }
        }
        else
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
        _exit.OnClick += ExitEditor;
    }

    protected override void OnDisposeView()
    {
        _verifySolution.onClick.AddListener(VerifySolution);
        _loadSavedCode.onClick.RemoveListener(GetSavedCode);
        _resetCode.onClick.RemoveListener(ResetCode);
        _exit.OnClick -= ExitEditor;
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
        _verdictCache = await SubmitSolution(_codeEditor.Task.Id, sourceCode);

        // Force select Result section.
        State.Section = new ResultSection(Lifetime)
        {
            Verdict = _verdictCache
        };
    }

    private async UniTask<Verdict> SubmitSolution(TaskId taskId, string sourceCode)
    {
        if (IsSuccessSubmitCheat)
        {
            var verdict = new Verdict()
            {
                TaskId = taskId,
                Solution = sourceCode,
                TestCases = new TestCasesVerdict()
                {
                    Values = new Dictionary<string, TestCaseVerdict>()
                }
            };
            _client.VerdictHistoryService.Add(verdict, DateTime.Now);
            return verdict;
        }

        var result = _client.Player != null
            ? await _client.Player.Tasks[_codeEditor.Task.Id].SubmitSolution(sourceCode).ToReportProgressStatus(State, "Выполняется...")
            : await _client.SubmitAnonymousSolution(_codeEditor.Task.Id, sourceCode).ToReportProgressStatus(State, "Выполняется...");

        if (result.IsSuccess)
        {
            return result.Value;
        }
        else
        {
            Debug.LogError(result.Error);
            return new NativeFailure()
            {
                Error = result.Error,
                Solution = sourceCode,
                TaskId = _codeEditor.Task.Id,
                TestCases = new TestCasesVerdict()
            };
        }
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