using TMPro;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Input_TMP : UIBehaviour, ITextField
{
    [SerializeField] private TMP_InputField inputField;

    public string Text
    {
        get => inputField.text;
        set => inputField.text = value;
    }
}