public interface ILoadWindow
{
    public void StartLoading();
    public void StopLoading();
}

public class LoadWindow : ILoadWindow
{
    public void StartLoading() => LoadWindowView.Instance.StartLoading();
    public void StopLoading() => LoadWindowView.Instance.StopLoading();
}
