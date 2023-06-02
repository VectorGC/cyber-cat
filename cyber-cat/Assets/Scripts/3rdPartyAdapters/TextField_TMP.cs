using TMPro;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextField_TMP : UIBehaviour, ITextField
{
    [SerializeField] private TMP_Text textContent;

    public string Text
    {
        get => textContent.text;
        set => textContent.SetText(value);
    }
}