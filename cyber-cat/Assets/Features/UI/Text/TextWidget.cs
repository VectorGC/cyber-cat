using UnityEngine;
using UnityEngine.UI;

public readonly struct TextWidget
{
    private readonly Text _text;

    public TextWidget(Text text)
    {
        _text = text;
    }

    public string Text
    {
        get => _text.text;
        set => _text.text = value;
    }

    public Color Color
    {
        get => _text.color;
        set => _text.color = value;
    }
}

public static class TextWidgetExtensions
{
    public static TextWidget ToTextWidget(this Text text)
    {
        return new TextWidget(text);
    }
}