using UniMob;
using UnityEngine;
using UnityEngine.UI;

public class SignInWithVK : LifetimeUIBehaviourV2<AccountState>
{
    [SerializeField] private Button _button;
    [Atom] protected override AccountState State { get; set; }

    protected override void OnInit()
    {
        _button.W().Clicked += OnClicked;
    }

    protected override void OnDispose()
    {
        _button.W().Clicked -= OnClicked;
    }

    protected override void OnUpdate()
    {
        var button = _button.W();
        if (State == null)
            return;

        button.Enable = !State.IsSignedIn;
    }

    private void OnClicked()
    {
        State?.SignIn();
    }
}