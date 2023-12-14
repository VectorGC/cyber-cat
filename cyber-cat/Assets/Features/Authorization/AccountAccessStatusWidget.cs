using UniMob;
using UnityEngine;
using UnityEngine.UI;

public class AccountAccessStatusWidget : LifetimeUIBehaviourV2<AccountState>
{
    [SerializeField] private Text _greetings;

    [Atom] protected override AccountState State { get; set; }

    protected override void OnUpdate()
    {
        var textWidget = _greetings.W();
        if (State == null)
        {
            textWidget.Text = "Получаем доступ...";
            return;
        }

        if (State.IsSignedIn)
        {
            textWidget.Text = $"Доступ получен: {State.User.FirstName}";
            textWidget.Color = Color.green;
        }
        else
        {
            textWidget.Text = "Доступ ограничен";
            textWidget.Color = Color.red;
        }
    }
}