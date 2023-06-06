using System;
using Models;
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

    private ICodeEditorService _codeEditorService;

    protected override async void Start()
    {
        _codeEditorService = GameManager.Instance.CodeEditor;
        var currentTask = await _codeEditorService.GetCurrentTask();

        _taskDescription.Task = currentTask;
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
        var verdict = await _codeEditorService.VerifySolution(sourceCode);

        _console.Value.Log(verdict.ToString());
    }

    private async void GetSavedCode()
    {
        var sourceCode = await _codeEditorService.GetSavedCode();
        _codeEditorView.Value.SourceCode = sourceCode;
    }

    private void ExitEditor()
    {
        _codeEditorService.CloseEditor().Forget();
    }
}