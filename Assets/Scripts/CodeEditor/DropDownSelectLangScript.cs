using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DropDownSelectLangScript : MonoBehaviour
{
    public TMP_InputField textArea;
    public TMP_Text pythonText;
    public TMP_Text cText;
    //public GameObject CodeEditor;

    public void SelectLang(int value)
    {
        switch (value)
        {
            case 0: PythonSelect(); break;
            case 1: CSelect(); break;
        }
    }
    private void PythonSelect()
    {
        LanguageType.SetLang("Python");
        textArea.text = pythonText.text;
    }
    private void CSelect()
    {
        LanguageType.SetLang("C");
        textArea.text = cText.text;
    }
}
public static class LanguageType
{
    private static string lang;
    public static void SetLang(string line)
    {
        lang = line;
    }
    public static string GetLang()
    {
        return lang;
    }
}
// вопросы
// 1. ¬ыбор €зыка в другой версионной ветке
// 2. ѕереписать скрипт по изменению подсветки
// 3. рендер текста на dropdown
// 4. xaml?
