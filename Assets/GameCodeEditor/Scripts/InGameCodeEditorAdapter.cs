using System;
using CodeEditorModels.ProgLanguages;
using InGameCodeEditor;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(InGameCodeEditor.InGameCodeEditor))]
public class InGameCodeEditorAdapter : MonoBehaviour
{
    private InGameCodeEditor.InGameCodeEditor _inGameCodeEditor;

    // TODO: Use SerializableDictionary here.
    [SerializeField] private CodeLanguageTheme cLanguageTheme;
    [SerializeField] private CodeLanguageTheme pythonLanguageTheme;
    [SerializeField] private CodeLanguageTheme pascalLanguageTheme;

    private IDisposable _unsubscriber;

    private void Awake()
    {
        TryGetComponent(out _inGameCodeEditor);
    }

    private void OnEnable()
    {
        _unsubscriber = MessageBroker.Default.Receive<ProgLanguageChanged>().Subscribe(OnProgLanguageChanged);
    }

    private void OnDisable()
    {
        _unsubscriber.Dispose();
    }

    private void OnProgLanguageChanged(ProgLanguageChanged msg)
    {
        var value = msg.Text;
        value = value.Replace("\r", "");
        _inGameCodeEditor.Text = value;

        switch (msg.Language)
        {
            case ProgLanguage.C:
                _inGameCodeEditor.LanguageTheme = cLanguageTheme;
                break;
            case ProgLanguage.Python:
                _inGameCodeEditor.LanguageTheme = pythonLanguageTheme;
                break;
            case ProgLanguage.Pascal:
                _inGameCodeEditor.LanguageTheme = pascalLanguageTheme;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        _inGameCodeEditor.Refresh();
    }
}