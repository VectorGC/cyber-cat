using UnityEngine;

public interface IText
{
    string Text { get; set; }
    Color Color { get; set; }
    void SetText(string handler);
}