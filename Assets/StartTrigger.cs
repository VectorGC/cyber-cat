using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : Trigger
{
    [Header("Приветствие")]
    [SerializeField] private string _title1;
    [SerializeField] private string _description1;
    [SerializeField] private string _buttonText1;

    [Header("Что вообще делать")]
    [SerializeField] private string _title2;
    [SerializeField] private string _description2;
    [SerializeField] private string _buttonText2;

    [Header("Что делать сейчас")]
    [SerializeField] private string _title3;
    [SerializeField] private string _description3;
    [SerializeField] private string _buttonText3;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        transform.position = Player.transform.position;
    }

    private void Hello()
    {
        string[] buttonText = new string[]
        {
            _buttonText1
        };
        UnityEngine.Events.UnityAction[] action = new UnityEngine.Events.UnityAction[]
        {
            Goal
        };
        ModalPanel.Instance.MessageBos(ModalPanel.Instance.InfoIcon, _title1, _description1, true, 1, action, buttonText);
    }

    private void Goal()
    {
        string[] buttonText = new string[]
        {
            _buttonText1
        };
        UnityEngine.Events.UnityAction[] action = new UnityEngine.Events.UnityAction[]
        {
            WhatToDoNow
        };
        ModalPanel.Instance.MessageBos(ModalPanel.Instance.InfoIcon, _title2, _description2, true, 1, action, buttonText);
    }

    private void WhatToDoNow()
    {
        string[] buttonText = new string[]
        {
            _buttonText3
        };
        UnityEngine.Events.UnityAction[] action = new UnityEngine.Events.UnityAction[]
        {
            StartMessageOK
        };
        ModalPanel.Instance.MessageBos(ModalPanel.Instance.InfoIcon, _title3, _description3, true, 1, action, buttonText);
    }

    private void StartMessageOK()
    {

    }

    public override void Enter()
    {
        Hello();
    }
}
