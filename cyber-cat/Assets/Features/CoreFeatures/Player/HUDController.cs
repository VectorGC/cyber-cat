using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HUDController : UIBehaviour
{
    [SerializeField] private Text _hintText;

    public string HintText
    {
        get => _hintText.text;
        set => _hintText.text = value;
    }
}