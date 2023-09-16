using System;

internal class SimpleModalWindowAdapter : IModal
{
    public event Action Closed
    {
        add => _window.Closed += value;
        remove => _window.Closed -= value;
    }

    public bool IsShow => _window.IsShow;

    private readonly SimpleModalWindow _window;

    public SimpleModalWindowAdapter(SimpleModalWindow window)
    {
        _window = window;
    }

    public void Show()
    {
        if (_window == null)
        {
            throw new NotSupportedException("Can't show modal twice");
        }

        _window.Show();
    }
}