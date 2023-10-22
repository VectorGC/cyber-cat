using System;
using UnityEngine.UI;

public interface IButton
{
    event Action Clicked;
    void SetActiveHighlight(bool isHighlight);
}

[Serializable]
public class IButtonInterface : SerializableInterface<IButton>, IButton
{
    public event Action Clicked
    {
        add => Value.Clicked += value;
        remove => Value.Clicked -= value;
    }

    public void SetActiveHighlight(bool isHighlight)
    {
        Value.SetActiveHighlight(isHighlight);
    }
}

public readonly struct ButtonWidget
{
    public event Action Clicked
    {
        add => _button.onClick.AddListener(value.Invoke);
        remove => _button.onClick.RemoveListener(value.Invoke);
    }

    private readonly Button _button;

    public ButtonWidget(Button button)
    {
        _button = button;
    }

    public bool Enable
    {
        get => _button.gameObject.activeSelf;
        set => _button.gameObject.SetActive(value);
    }
}

public static class ButtonWidgetExtensions
{
    public static ButtonWidget Widget(this Button button)
    {
        return new ButtonWidget(button);
    }
}