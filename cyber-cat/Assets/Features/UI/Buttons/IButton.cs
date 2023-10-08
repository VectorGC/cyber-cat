using System;

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