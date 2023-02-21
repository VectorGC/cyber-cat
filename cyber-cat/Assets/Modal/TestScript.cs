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
        string[] names = new string[] { "Да", "Нет" };
        _modalpanel.MessageBos(_questionIcon, "Очень важный вопрос", "Вы хотите пройти обучение?", true, 2, actions, names);
    }

    public void TestGoodMan()
    {
        UnityAction[] actions = new UnityAction[] { GoodMan};
        string[] names = new string[] { "Да"};
        _modalpanel.MessageBos(_errorIcon, "Очень хороший человек", "Вы знали, что вы молодец", true, 1, actions, names);
    }

    public void TestFruits()
    {
        UnityAction[] actions = new UnityAction[] { Apple, Orange, Mango };
        string[] names = new string[] { "Яблоко", "Апельсин", "Манго" };
        _modalpanel.MessageBos(_errorIcon, "Очень хороший человек", "Вы знали, что вы молодец", true, 3, actions, names);
    }

    private void DoTutorial()
    {
        Debug.Log("Согашение к туториалу");
    }

    private void DoNotTutorial()
    {
        Debug.Log("Отказ от туториала");
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
