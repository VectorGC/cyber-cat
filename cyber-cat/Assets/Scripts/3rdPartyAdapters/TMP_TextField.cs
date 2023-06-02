using TMPro;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class TMP_TextField : UIBehaviour, ITextField
{
    [SerializeField] private TMP_Text textContent;

    public string Text
    {
        get => textContent.text;
        set => textContent.SetText(value);
    }
}