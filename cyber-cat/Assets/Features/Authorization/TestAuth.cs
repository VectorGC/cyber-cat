using UnityEngine;
using Zenject;

public class TestAuth : MonoBehaviour
{
    private AuthorizationPresenter _presenter;

    [Inject]
    public void Construct(AuthorizationPresenter presenter)
    {
        _presenter = presenter;
    }

    public void OnGUI()
    {
        if (GUILayout.Button("Open"))
        {
            _presenter.Show().Forget();
        }

        if (GUILayout.Button("Close"))
        {
            _presenter.Hide().Forget();
        }
    }
}