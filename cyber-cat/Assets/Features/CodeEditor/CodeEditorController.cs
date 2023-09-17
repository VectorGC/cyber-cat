using ApiGateway.Client.Internal.Tasks.Verdicts;
using Models;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class CodeEditorController : UIBehaviour
{
    [SerializeField] private TaskDescription _taskDescription;
    [SerializeField] private CodeEditorView _codeEditorView;
    [SerializeField] private CodeConsoleView _console;
    [Header("Buttons")] [SerializeField] private Button _verifySolution;
    [SerializeField] private Button _loadSavedCode;
    [SerializeField] private Button _exit;

    private ICodeEditor _codeEditor;

    [Inject]
    private void Construct(ICodeEditor codeEditor)
    {
        _codeEditor = codeEditor;

        _taskDescription.Task = _codeEditor.Task;
        _codeEditorView.Language = LanguageProg.Cpp;
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
        var sourceCode = _codeEditorView.SourceCode;
        var verdict = await _codeEditor.Task.VerifySolution(sourceCode);

        switch (verdict)
        {
            case Success success:
                _console.LogSuccess(success.ToString());
                break;
            case Failure failure:
                _console.LogError(failure.ToString());
                break;
        }
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