using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HUDController : UIBehaviour
{
    [SerializeField] private Text _hintText;

    public string HintText
    {
        get => _hintText.text;
        set => _hintTextOnFrame = value;
    }

    private string _hintTextOnFrame;

    private void LateUpdate()
    {
        if (_hintTextOnFrame != _hintText.text)
        {
            _hintText.text = _hintTextOnFrame;
        }

        _hintTextOnFrame = string.Empty;
    }
}