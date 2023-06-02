using InGameCodeEditor;
using Models;
using UnityEngine;
using UnityEngine.EventSystems;

public class InGameCodeEditorAdapter : UIBehaviour, ICodeEditorView
{
    [SerializeField] private InGameCodeEditor.InGameCodeEditor _inGameCodeEditor;

    [Header("Themes")] [SerializeField] private CodeLanguageTheme _cpp;
    [SerializeField] private CodeLanguageTheme _python;
    [SerializeField] private CodeLanguageTheme _pascal;

    public LanguageProg Language
    {
        set => SetLanguage(value);
    }

    public string SourceCode
    {
        get => _inGameCodeEditor.Text;
        set
        {
            var text = value.Replace("\r", "");
            _inGameCodeEditor.Text = text;
            _inGameCodeEditor.Refresh();
        }
    }

    private void SetLanguage(LanguageProg language)
    {
        switch (language)
        {
            case LanguageProg.Cpp:
                _inGameCodeEditor.LanguageTheme = _cpp;
                break;
            case LanguageProg.Python:
                _inGameCodeEditor.LanguageTheme = _python;
                break;
            case LanguageProg.Pascal:
                _inGameCodeEditor.LanguageTheme = _pascal;
                break;
        }

        _inGameCodeEditor.Refresh();
    }
}