using System;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

public class ErrorMessageView : UIBehaviour, IObserver<Exception>
{
    [SerializeField] private TMP_Text text;

    protected override void Awake()
    {
        MessageBroker.Default.Receive<Exception>().Subscribe(OnError);
    }

    protected override void Start()
    {
        text.text = string.Empty;
    }

    public void OnCompleted()
    {
        text.text = string.Empty;
    }

    public void SetGoodColor()
    {
        text.color = Color.green;
    }

    public void SetBadColor()
    {
        text.color = Color.red;
    }

    public void OnError(Authentication.RequestTokenException error) => text.text = _errors[int.Parse(error.Message)] + $" ( од ошибки {error.Message})";

    public void OnError(Exception error) => text.text = error.Message;

    public void OnNext(Authentication.RequestTokenException value) => OnError(value);

    public void OnNext(Exception value) => OnError(value);

    private static Dictionary<int, string> _errors = new Dictionary<int, string>
    {
        { 0, "ќшибок нет"},
        { 1, "¬нутренн€€ ошибка сервера"},
        { 2, "¬нутренн€€ ошибка"},
        { 100, "Email не был введен"},
        { 101, "Email был введен с ошибкой"},
        { 102, "Email не зарегстрирован"},
        { 103, "“акой email уже зарегстрирован"},
        { 200, "ѕароль не был введен"},
        { 201, "ѕароль должен содержать хот€ бы 6 символов"},
        { 202, "ѕароль неправильный"},
        { 300, "¬нутренн€€ ошибка"},
        { 301, "¬нутренн€€ ошибка"},
        { 302, "¬нутренн€€ ошибка"},
        { 303, "¬нутренн€€ ошибка"},
        { 304, "¬нутренн€€ ошибка"},
        { 400, "¬нутренн€€ ошибка"},
        { 401, "¬нутренн€€ ошибка"},
        { 402, "¬нутренн€€ ошибка"},
        { 500, "¬нутренн€€ ошибка"},
        { 501, "¬нутренн€€ ошибка"},
        { 502, "¬нутренн€€ ошибка"},
        { 503, "¬нутренн€€ ошибка"},
        { 504, "¬нутренн€€ ошибка"},
        { 505, "¬нутренн€€ ошибка"},
        { 506, "¬нутренн€€ ошибка"},
        { 507, "¬нутренн€€ ошибка"},
        { 508, "¬нутренн€€ ошибка"},
        { 509, "¬нутренн€€ ошибка"},
        { 600, "¬нутренн€€ ошибка"},
        { 601, "ƒанный €зык не может быть обработан"},
        { 700, "¬нутренн€€ ошибка"},
        { 701, "»м€ должно содержать менее 128 символов"},
        { 800, "¬нутренн€€ ошибка"},
        { 801, "¬нутренн€€ ошибка"},
        { 802, "¬нутренн€€ ошибка"},
        { 803, "¬нутренн€€ ошибка"},
    };
}