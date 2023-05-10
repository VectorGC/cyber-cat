using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestScript : MonoBehaviour
{
    [SerializeField] private Sprite _errorIcon;
    [SerializeField] private Sprite _questionIcon;

    private ModalPanel _modalpanel;

    private void Awake()
    {
        _modalpanel = new ModalPanel();
    }

    public void TestTutorial()
    {
        UnityAction[] actions = new UnityAction[] {DoTutorial, DoNotTutorial};
        string[] names = new string[] { "��", "���" };
        _modalpanel.MessageBos(_questionIcon, "����� ������ ������", "�� ������ ������ ��������?", true, 2, actions, names);
    }

    public void TestGoodMan()
    {
        UnityAction[] actions = new UnityAction[] { GoodMan};
        string[] names = new string[] { "��"};
        _modalpanel.MessageBos(_errorIcon, "����� ������� �������", "�� �����, ��� �� �������", true, 1, actions, names);
    }

    public void TestFruits()
    {
        UnityAction[] actions = new UnityAction[] { Apple, Orange, Mango };
        string[] names = new string[] { "������", "��������", "�����" };
        _modalpanel.MessageBos(_errorIcon, "����� ������� �������", "�� �����, ��� �� �������", true, 3, actions, names);
    }

    private void DoTutorial()
    {
        Debug.Log("��������� � ���������");
    }

    private void DoNotTutorial()
    {
        Debug.Log("����� �� ���������");
    }

    private void GoodMan()
    {
        Debug.Log("Very good and very well");
    }

    private void Apple()
    {
        Debug.Log("Apple");
    }

    private void Orange()
    {
        Debug.Log("Orange");
    }

    private void Mango()
    {
        Debug.Log("Mango");
    }
}
