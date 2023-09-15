using System;

internal class SimpleModalWindowAdapter : IModal
{
    public bool IsShow => _window && _window.Visible;

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