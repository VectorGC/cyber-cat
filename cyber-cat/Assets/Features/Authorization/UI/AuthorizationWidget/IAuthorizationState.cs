using UniMob;

public interface IAuthorizationState : ILifetimeScope
{
    string Title { get; }
    string Error { get; }
    string Email { get; set; }
    string Password { get; set; }
    string ButtonText { get; }
    void OnButtonClicked();
}