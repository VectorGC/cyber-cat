using CodeEditorModels.ProgLanguages;
using TMPro;
using UnityEngine.EventSystems;

public class ProgLanguagePicker : UIBehaviour
{
    // TODO: Move to ScriptableObject.
    public TMP_Text pythonTemplateText;
    public TMP_Text cTemplateText;
    public TMP_Text pascalTemplateText;

    protected override void Start()
    {
        SelectLanguage(ProgLanguage.C, cTemplateText.text);
    }

    public void SelectLanguage(int value)
    {
        switch (value)
        {
            case 0:
                SelectLanguage(ProgLanguage.C, cTemplateText.text);
                break;
            case 1:
                SelectLanguage(ProgLanguage.Python, pythonTemplateText.text);
                break;
            case 2:
                SelectLanguage(ProgLanguage.Pascal, pascalTemplateText.text);
                break;
        }
    }

    private static void SelectLanguage(ProgLanguage language, string text)
    {
        new ProgLanguageChanged(language, text).Publish();
    }
}