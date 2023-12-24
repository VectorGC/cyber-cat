public interface ISecondAuthorizationButton
{
    string SecondButtonText { get; }
    void OnSecondButtonClicked();
}