using System;
using ApiGateway.Client.Internal.Tasks.Verdicts;
using ApiGateway.Client.Models;
using Cysharp.Threading.Tasks;
using Models;
using Shared.Models.Models;
using UniMob;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CodeEditorController : LifetimeMonoBehaviour
{
    [SerializeField] private SerializableInterface<IText> _taskDescription;
    [SerializeField] private CodeEditorView _codeEditorView;
    [SerializeField] private CodeConsoleController _console;
    [SerializeField] private CodeEditorStatusBar _statusBar;
    [Header("Buttons")] [SerializeField] private Button _verifySolution;
    [SerializeField] private Button _loadSavedCode;
    [SerializeField] private Button _exit;

    [Header("Debug")] [SerializeField] private bool _loadTutorial;

    private ICodeEditor _codeEditor;
    private CodeEditorState _state;
    private TestCases _testCasesCache;
    private VerdictV2 _verdictCache;

    [Inject]
    private async void Construct(ICodeEditor codeEditor)
    {
        _codeEditor = codeEditor;

        if (Application.isEditor && Application.isPlaying && _loadTutorial)
        {
            var typedEditor = (CodeEditor) codeEditor;
            await typedEditor.LoadTutorialCheat();
        }

        var descriptionHandler = codeEditor.Task.GetDescription();
        _taskDescription.Value.SetTextAsync(descriptionHandler);
        _codeEditorView.Language = LanguageProg.Cpp;
    }

    protected override async void Start()
    {
        base.Start();

        await UniTask.WaitUntil(() => _codeEditor.Task != null);

        _testCasesCache = await _codeEditor.Task.GetTestCases();
        _state = new CodeEditorState(Lifetime)
        {
            Section = new TestCasesSection(Lifetime)
            {
                TestCases = _testCasesCache
            }
        };
        _console.State = _state;
        _statusBar.State = _state;

        _state.SectionChanged += OnSectionChanged;
    }

    protected override void OnDestroy()
    {
        _state.SectionChanged -= OnSectionChanged;

        base.OnDestroy();
    }

    private void OnSectionChanged()
    {
        OnSectionChangedAsync().Forget();
    }

    private async UniTaskVoid OnSectionChangedAsync()
    {
        switch (_state.Section)
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

    private void OnEnable()
    {
        _verifySolution.onClick.AddListener(VerifySolution);
        _loadSavedCode.onClick.AddListener(GetSavedCode);
        _exit.onClick.AddListener(ExitEditor);
    }

    private void OnDisable()
    {
        _verifySolution.onClick.AddListener(VerifySolution);
        _loadSavedCode.onClick.RemoveListener(GetSavedCode);
        _exit.onClick.RemoveListener(ExitEditor);
    }

    private async void VerifySolution()
    {
        var sourceCode = _codeEditorView.SourceCode;
        var verdict = await _codeEditor.Task.VerifySolution(sourceCode).ToReportProgressStatus(_state, "Проверяем...");
        _verdictCache = await _codeEditor.Task.VerifySolutionV2(sourceCode).ToReportProgressStatus(_state, "Проверяем v2...");

        /*
        switch (verdict)
        {
            case Success success:
                _console.LogSuccess(success.ToString());
                break;
            case Failure failure:
                _console.LogError(failure.ToString());
                break;
        }
        */
    }

    private async void GetSavedCode()
    {
        var sourceCode = await _codeEditor.Task.GetLastSavedSolution();
        _codeEditorView.SourceCode = sourceCode;
    }

    private void ExitEditor()
    {
        _codeEditor.Close();
    }
}