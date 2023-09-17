using InGameCodeEditor;
using Models;
using UnityEngine;

public class InGameCodeEditorAdapter : CodeEditorView
{
    [SerializeField] private InGameCodeEditor.InGameCodeEditor _inGameCodeEditor;

    [Header("Themes")] [SerializeField] private CodeLanguageTheme _cpp;
    [SerializeField] private CodeLanguageTheme _python;
    [SerializeField] private CodeLanguageTheme _pascal;

    private LanguageProg _language;

    public override LanguageProg Language
    {
        get => _language;
        set => SetLanguage(value);
    }

    public override string SourceCode
    {
        get => _inGameCodeEditor.Text;
        set
        {
            var text = value.Replace("\r", "");
            _inGameCodeEditor.Text = text;
        }
    }

    private void SetLanguage(LanguageProg language)
    {
        _language = language;
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
    }
}