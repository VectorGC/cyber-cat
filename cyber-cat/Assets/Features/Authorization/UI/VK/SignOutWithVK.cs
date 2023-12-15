using UniMob;
using UnityEngine;
using UnityEngine.UI;

public class SignOutWithVK : LifetimeUIBehaviourV2<VkAuthService>
{
    [SerializeField] private Button _button;
    [Atom] protected override VkAuthService State { get; set; }

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

        button.Enable = State.IsSignedIn;
    }

    private void OnClicked()
    {
        State?.SignOut().Forget();
    }
}