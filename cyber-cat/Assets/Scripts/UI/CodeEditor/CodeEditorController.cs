using System;
using Models;
using Shared.Dto;
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

    protected override void Start()
    {
        _codeEditorService = GameManager.Instance.CodeEditor;
        var currentTask = _codeEditorService.CurrentTask;
        if (currentTask == null)
        {
            if (!Debug.isDebugBuild)
            {
                throw new ArgumentNullException(nameof(_codeEditorService.CurrentTask));
            }

            currentTask = new TaskCode(new TaskDto
            {
                Name = "Тестовая задача",
                Description = "Выполните сначала А, потом Б. Входные данные 123 и массив из двух чисел. Выходные данные строка \"Кот\""
            });
        }

        _taskDescription.Task = currentTask;
        _codeEditorView.Value.Language = LanguageProg.Cpp;
    }

    protected override void OnEnable()
    {
        Time.timeScale = 0f;

        _verifySolution.onClick.AddListener(VerifySolution);
        _loadSavedCode.onClick.AddListener(LoadSavedCode);
        _exit.onClick.AddListener(ExitEditor);
    }

    protected override void OnDisable()
    {
        Time.timeScale = 1f;

        _verifySolution.onClick.AddListener(VerifySolution);
        _loadSavedCode.onClick.RemoveListener(LoadSavedCode);
        _exit.onClick.RemoveListener(ExitEditor);
    }

    private async void VerifySolution()
    {
        var task = _codeEditorService.CurrentTask;
        var sourceCode = _codeEditorView.Value.SourceCode;
        var verdict = await _codeEditorService.VerifySolution(task, sourceCode);

        _console.Value.Log(verdict.ToString());
    }

    private void LoadSavedCode()
    {
    }

    private void ExitEditor()
    {
        _codeEditorService.CloseEditor().Forget();
    }
}