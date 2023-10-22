using UniMob;
using UnityEngine;
using UnityEngine.UI;

public class SignOutWithVK : LifetimeUIBehaviourV2<AccountState>
{
    [SerializeField] private Button _button;
    [Atom] protected override AccountState State { get; set; }

    protected override void OnInit()
    {
        _button.Widget().Clicked += OnClicked;
    }

    protected override void OnDispose()
    {
        _button.Widget().Clicked -= OnClicked;
    }

    protected override void OnUpdate()
    {
        var button = _button.Widget();
        if (State == null)
            return;

        button.Enable = State.IsSignedIn;
    }

    private void OnClicked()
    {
        State?.SignOut();
    }
}