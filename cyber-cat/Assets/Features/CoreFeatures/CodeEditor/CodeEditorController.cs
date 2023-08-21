using ApiGateway.Client;
using ApiGateway.Client.Factory;
using Cysharp.Threading.Tasks;
using Features.GameManager;
using Models;
using Shared.Models.Models;
using TNRD;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CodeEditorController : UIBehaviour
{
    [SerializeField] private TaskDescription _taskDescription;
    [SerializeField] private SerializableInterface<ICodeEditorView> _codeEditorView;
    [SerializeField] private SerializableInterface<ICodeConsoleView> _console;
    [Header("Buttons")] [SerializeField] private Button _verifySolution;
    [SerializeField] private Button _loadSavedCode;
    [SerializeField] private Button _exit;

    private IAuthorizedClient _client;
    private ICodeEditor _codeEditor;

    public async UniTask Construct(ICodeEditor codeEditor)
    {
        _codeEditor = codeEditor;

        _client = await ServerClientFactory.UseCredentials("cat", "cat").Create(GameConfig.ServerEnvironment);
        var task = await _client.Tasks.GetTask(_codeEditor.TaskId);
        _taskDescription.Task = task;
        _codeEditorView.Value.Language = LanguageProg.Cpp;
    }

    protected override void OnEnable()
    {
        Time.timeScale = 0f;

        _verifySolution.onClick.AddListener(VerifySolution);
        _loadSavedCode.onClick.AddListener(GetSavedCode);
        _exit.onClick.AddListener(ExitEditor);
    }

    protected override void OnDisable()
    {
        Time.timeScale = 1f;

        _verifySolution.onClick.AddListener(VerifySolution);
        _loadSavedCode.onClick.RemoveListener(GetSavedCode);
        _exit.onClick.RemoveListener(ExitEditor);
    }

    private async void VerifySolution()
    {
        var sourceCode = _codeEditorView.Value.SourceCode;
        var verdict = await _client.JudgeService.VerifySolution(_codeEditor.TaskId, sourceCode);

        switch (verdict.Status)
        {
            case VerdictStatus.Success:
                _console.Value.LogSuccess($"{verdict.Status}: {verdict.TestsPassed} test passed");
                break;
            case VerdictStatus.Failure:
                _console.Value.LogError($"{verdict.Status}: {verdict.TestsPassed} test passed. Error: {verdict.Error}");
                break;
        }
    }

    private async void GetSavedCode()
    {
        var sourceCode = await _client.SolutionService.GetSavedCode(_codeEditor.TaskId);
        _codeEditorView.Value.SourceCode = sourceCode;
    }

    private void ExitEditor()
    {
        _codeEditor.Close();
    }
}