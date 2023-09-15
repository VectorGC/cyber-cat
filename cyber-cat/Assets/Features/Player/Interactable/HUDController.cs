using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public interface IHud
{
    string HintText { get; set; }
}

public class HUDController : UIBehaviour, IHud
{
    [SerializeField] private Text _hintText;

    public string HintText
    {
        get => _hintText.text;
        set
        {
            _hintTextOnDelay = value;
            _delay = 0.1f;
        }
    }

    private string _hintTextOnDelay;
    private float _delay;

    private void LateUpdate()
    {
        if (_delay > 0)
        {
            _delay -= Time.deltaTime;
            if (_hintTextOnDelay != _hintText.text)
            {
                _hintText.text = _hintTextOnDelay;
            }
        }
        else
        {
            _hintText.text = string.Empty;
        }
    }
}