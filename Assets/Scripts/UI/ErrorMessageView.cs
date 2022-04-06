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

    public void OnError(Authentication.RequestTokenException error) => text.text = _errors[int.Parse(error.Message)] + $" (��� ������ {error.Message})";

    public void OnError(Exception error) => text.text = error.Message;

    public void OnNext(Authentication.RequestTokenException value) => OnError(value);

    public void OnNext(Exception value) => OnError(value);

    private static Dictionary<int, string> _errors = new Dictionary<int, string>
    {
        { 0, "������ ���"},
        { 1, "���������� ������ �������"},
        { 2, "���������� ������"},
        { 100, "Email �� ��� ������"},
        { 101, "Email ��� ������ � �������"},
        { 102, "Email �� ��������������"},
        { 103, "����� email ��� ��������������"},
        { 200, "������ �� ��� ������"},
        { 201, "������ ������ ��������� ���� �� 6 ��������"},
        { 202, "������ ������������"},
        { 300, "���������� ������"},
        { 301, "���������� ������"},
        { 302, "���������� ������"},
        { 303, "���������� ������"},
        { 304, "���������� ������"},
        { 400, "���������� ������"},
        { 401, "���������� ������"},
        { 402, "���������� ������"},
        { 500, "���������� ������"},
        { 501, "���������� ������"},
        { 502, "���������� ������"},
        { 503, "���������� ������"},
        { 504, "���������� ������"},
        { 505, "���������� ������"},
        { 506, "���������� ������"},
        { 507, "���������� ������"},
        { 508, "���������� ������"},
        { 509, "���������� ������"},
        { 600, "���������� ������"},
        { 601, "������ ���� �� ����� ���� ���������"},
        { 700, "���������� ������"},
        { 701, "��� ������ ��������� ����� 128 ��������"},
        { 800, "���������� ������"},
        { 801, "���������� ������"},
        { 802, "���������� ������"},
        { 803, "���������� ������"},
    };
}