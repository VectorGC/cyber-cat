using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using TMPro;

public class ModalPanel : MonoBehaviour
{
    [SerializeField] private TextMeshPro_TextShared _title; //The Modal Window Title
    [SerializeField] private TextMeshPro_TextShared _question; //The Modal Window Question (or statement)
    [SerializeField] private Button _button1; //The first button
    [SerializeField] private Button _button2; //The second button
    [SerializeField] private Button _button3; //The third button
    [SerializeField] private Image _iconImage; //The Icon Image, if any

    [SerializeField] private Sprite _errorIcon;
    public Sprite ErrorIcon => _errorIcon;
    [SerializeField] private Sprite _questionIcon;
    public Sprite QuestionIcon => _questionIcon;
    [SerializeField] private Sprite _infoIcon;
    public Sprite InfoIcon => _infoIcon;
    [SerializeField] private Sprite _exclamationIcon;
    public Sprite ExclamationIcon => _exclamationIcon;

    private UnityEvent _onEndOfDialog = new UnityEvent();
    private UnityEvent _onStartOfDialog = new UnityEvent();

    [SerializeField] private GameObject _modalPanelObject; //Reference to the Panel Object

    // Patch
    public static void ShowModalDialog(string title, string message, UnityAction okAction)
    {
        //Time.timeScale = 0f;
        var modalPanel = FindObjectOfType<ModalPanel>();
        if (!modalPanel)
        {
            return;
        }

        modalPanel.MessageBox(null, title, message, () => { }, () => { }, () => { },
            () =>
            {
                //Time.timeScale = 1f;
                okAction.Invoke();
            }, false, "Ok");
    }

    public void MessageBox(Sprite IconPic, string Title, string Question, UnityAction YesEvent, UnityAction NoEvent,
        UnityAction CancelEvent, UnityAction OkEvent, bool IconActive, string MessageType)
    {
        _modalPanelObject.SetActive(true); //Activate the Panel; its default is "off" in the Inspector
        if (MessageType == "YesNoCancel") //If the user has asked for the Message Box type "YesNoCancel"
        {
            //Button1 is on the far left; Button2 is in the center and Button3 is on the right.  Each can be activated and labeled individually.
            _button1.onClick.RemoveAllListeners();
            _button1.onClick.AddListener(YesEvent);
            _button1.onClick.AddListener(ClosePanel);
            _button1.GetComponentInChildren<Text>().text = "Yes";
            _button2.onClick.RemoveAllListeners();
            _button2.onClick.AddListener(NoEvent);
            _button2.onClick.AddListener(ClosePanel);
            _button2.GetComponentInChildren<Text>().text = "No";
            _button3.onClick.RemoveAllListeners();
            _button3.onClick.AddListener(CancelEvent);
            _button3.onClick.AddListener(ClosePanel);
            _button3.GetComponentInChildren<Text>().text = "Cancel";
            _button1.gameObject.SetActive(true); //We always turn on ONLY the buttons we need, and leave the rest off.
            _button2.gameObject.SetActive(true);
            _button3.gameObject.SetActive(true);
        }

        if (MessageType == "YesNo") //If the user has asked for the Message Box type "YesNo"
        {
            _button1.onClick.RemoveAllListeners();
            _button2.onClick.RemoveAllListeners();
            _button2.onClick.AddListener(YesEvent);
            _button2.onClick.AddListener(ClosePanel);
            _button2.GetComponentInChildren<Text>().text = "Yes";
            _button3.onClick.RemoveAllListeners();
            _button3.onClick.AddListener(NoEvent);
            _button3.onClick.AddListener(ClosePanel);
            _button3.GetComponentInChildren<Text>().text = "No";
            _button1.gameObject.SetActive(false);
            _button2.gameObject.SetActive(true);
            _button3.gameObject.SetActive(true);
        }

        if (MessageType == "Ok") //If the user has asked for the Message Box type "Ok"
        {
            _button1.onClick.RemoveAllListeners();
            _button2.onClick.RemoveAllListeners();
            _button2.onClick.AddListener(OkEvent);
            _button2.onClick.AddListener(ClosePanel);
            _button2.GetComponentInChildren<TMP_Text>().text = "Ok";
            _button3.onClick.RemoveAllListeners();
            _button1.gameObject.SetActive(false);
            _button2.gameObject.SetActive(true);
            _button3.gameObject.SetActive(false);
        }

        this._title.text = Title; //Fill in the Title part of the Message Box
        this._question.text = Question; //Fill in the Question/statement part of the Messsage Box
        if (IconActive) //If the Icon is active (true)...
        {
            this._iconImage.gameObject.SetActive(true); //Turn on the icon,
            this._iconImage.sprite = IconPic; //and assign the picture.
        }
        else
        {
            this._iconImage.gameObject.SetActive(false); //Turn off the icon.
        }
    }

    public void MessageBos(Sprite iconPic, string title, string question, bool iconActive, int countOfButtons,
        UnityAction[] actions, string[] buttonNames, UnityAction[] onEnd = null, UnityAction[] onShow = null)
    {
        _modalPanelObject.SetActive(true);
        if (actions != null)
        {
            if (countOfButtons != actions.Length)
            {
                throw new System.ArgumentException();
            }
        }

        if (countOfButtons != buttonNames.Length)
        {
            throw new System.ArgumentException();
        }

        AddOnEndAndShow(onEnd, onShow);

        if (countOfButtons == 1)
        {
            _onStartOfDialog?.Invoke();
            //Button1 is on the far left; Button2 is in the center and Button3 is on the right.  Each can be activated and labeled individually.
            _button1.onClick.RemoveAllListeners();
            _button2.onClick.RemoveAllListeners();
            _button2.onClick.AddListener(ClosePanel);
            _button2.onClick.AddListener(actions?[0]);
            _button2.GetComponentInChildren<TextMeshPro_TextShared>().text = buttonNames[0];
            _button3.onClick.RemoveAllListeners();
            _button1.gameObject.SetActive(false); //We always turn on ONLY the buttons we need, and leave the rest off.
            _button2.gameObject.SetActive(true);
            _button3.gameObject.SetActive(false);
        }
        else if (countOfButtons == 2)
        {
            _onStartOfDialog?.Invoke();
            _button1.onClick.RemoveAllListeners();
            _button1.onClick.AddListener(ClosePanel);
            _button1.onClick.AddListener(actions?[0]);
            _button1.GetComponentInChildren<TextMeshPro_TextShared>().text = buttonNames[0];
            _button2.onClick.RemoveAllListeners();
            _button3.onClick.RemoveAllListeners();
            _button3.onClick.AddListener(ClosePanel);
            _button3.onClick.AddListener(actions?[1]);
            _button3.GetComponentInChildren<TextMeshPro_TextShared>().text = buttonNames[1];
            _button1.gameObject.SetActive(true);
            _button2.gameObject.SetActive(false);
            _button3.gameObject.SetActive(true);
        }
        else if (countOfButtons == 3)
        {
            _onStartOfDialog?.Invoke();
            _button1.onClick.RemoveAllListeners();
            _button1.onClick.AddListener(ClosePanel);
            _button1.onClick.AddListener(actions?[0]);
            _button1.GetComponentInChildren<TextMeshPro_TextShared>().text = buttonNames[0];
            _button2.onClick.RemoveAllListeners();
            _button2.onClick.AddListener(ClosePanel);
            _button2.onClick.AddListener(actions?[1]);
            _button2.GetComponentInChildren<TextMeshPro_TextShared>().text = buttonNames[1];
            _button3.onClick.RemoveAllListeners();
            _button3.onClick.AddListener(ClosePanel);
            _button3.onClick.AddListener(actions?[2]);
            _button3.GetComponentInChildren<TextMeshPro_TextShared>().text = buttonNames[2];
            _button1.gameObject.SetActive(true); //We always turn on ONLY the buttons we need, and leave the rest off.
            _button2.gameObject.SetActive(true);
            _button3.gameObject.SetActive(true);
        }
        else
        {
            throw new System.ArgumentException();
        }

        this._title.text = title; //Fill in the Title part of the Message Box
        this._question.text = question; //Fill in the Question/statement part of the Messsage Box
        if (iconActive) //If the Icon is active (true)...
        {
            this._iconImage.gameObject.SetActive(true); //Turn on the icon,
            this._iconImage.sprite = iconPic; //and assign the picture.
        }
        else
        {
            this._iconImage.gameObject.SetActive(false); //Turn off the icon.
        }
    }

    public void MessageBos(ModalInfo modalInfo, UnityAction[] onEnd = null, UnityAction[] onShow = null)
    {
        _modalPanelObject.SetActive(true);
        if (modalInfo.ButtonEvents != null)
        {
            if (modalInfo.CountOfButtons != modalInfo.ButtonEvents.Length)
            {
                throw new System.ArgumentException();
            }
        }

        if (modalInfo.CountOfButtons != modalInfo.ButtonText.Length)
        {
            throw new System.ArgumentException();
        }

        AddOnEndAndShow(onEnd, onShow);

        if (modalInfo.CountOfButtons == 1)
        {
            _onStartOfDialog?.Invoke();
            //Button1 is on the far left; Button2 is in the center and Button3 is on the right.  Each can be activated and labeled individually.
            _button1.onClick.RemoveAllListeners();
            _button2.onClick.RemoveAllListeners();
            _button2.onClick = modalInfo.ButtonEvents[0];
            _button2.onClick.AddListener(ClosePanel);
            _button2.GetComponentInChildren<TextMeshPro_TextShared>().text = modalInfo.ButtonText[0];
            _button3.onClick.RemoveAllListeners();
            _button1.gameObject.SetActive(false); //We always turn on ONLY the buttons we need, and leave the rest off.
            _button2.gameObject.SetActive(true);
            _button3.gameObject.SetActive(false);
        }
        else if (modalInfo.CountOfButtons == 2)
        {
            _onStartOfDialog?.Invoke();
            _button1.onClick.RemoveAllListeners();
            _button1.onClick = modalInfo.ButtonEvents[0];
            _button1.onClick.AddListener(ClosePanel);
            _button1.GetComponentInChildren<TextMeshPro_TextShared>().text = modalInfo.ButtonText[0];
            _button2.onClick.RemoveAllListeners();
            _button3.onClick.RemoveAllListeners();
            _button3.onClick = modalInfo.ButtonEvents[1];
            _button3.onClick.AddListener(ClosePanel);
            _button3.GetComponentInChildren<TextMeshPro_TextShared>().text = modalInfo.ButtonText[1];
            _button1.gameObject.SetActive(true);
            _button2.gameObject.SetActive(false);
            _button3.gameObject.SetActive(true);
        }
        else if (modalInfo.CountOfButtons == 3)
        {
            _onStartOfDialog?.Invoke();
            _button1.onClick.RemoveAllListeners();
            _button1.onClick = modalInfo.ButtonEvents[0];
            _button1.onClick.AddListener(ClosePanel);
            _button1.GetComponentInChildren<TextMeshPro_TextShared>().text = modalInfo.ButtonText[0];
            _button2.onClick.RemoveAllListeners();
            _button2.onClick = modalInfo.ButtonEvents[1];
            _button2.onClick.AddListener(ClosePanel);
            _button2.GetComponentInChildren<TextMeshPro_TextShared>().text = modalInfo.ButtonText[1];
            _button3.onClick.RemoveAllListeners();
            _button3.onClick = modalInfo.ButtonEvents[2];
            _button3.onClick.AddListener(ClosePanel);
            _button3.GetComponentInChildren<TextMeshPro_TextShared>().text = modalInfo.ButtonText[2];
            _button1.gameObject.SetActive(true); //We always turn on ONLY the buttons we need, and leave the rest off.
            _button2.gameObject.SetActive(true);
            _button3.gameObject.SetActive(true);
        }
        else
        {
            throw new System.ArgumentException();
        }

        this._title.text = modalInfo.Title;
        this._question.text = modalInfo.Description;
        this._iconImage.gameObject.SetActive(true);
        this._iconImage.sprite = modalInfo.Icon;
    }

    public void MessageBos(ModalInfo[] modalInfos, UnityAction[] onEnd = null, UnityAction[] onShow = null)
    {
        StartCoroutine(CycleWithTimer(modalInfos, onEnd, onShow));
    }

    private IEnumerator CycleWithTimer(ModalInfo[] modalInfos, UnityAction[] onEnd = null, UnityAction[] onShow = null)
    {
        for (int i = 0; i < modalInfos.Length;)
        {
            yield return null;
            if (!_modalPanelObject.activeInHierarchy)
            {
                yield return new WaitForSeconds(modalInfos[i].TimeBeforeShow);
                MessageBos(modalInfos[i], onEnd, onShow);
                i++;
            }
        }
    }

    private void AddOnEndAndShow(UnityAction[] onEnd, UnityAction[] onShow)
    {
        _onEndOfDialog?.RemoveAllListeners();
        if (onEnd != null)
        {
            for (int i = 0; i < onEnd.Length; i++)
            {
                _onEndOfDialog.AddListener(onEnd[i]);
            }
        }

        if (onShow != null)
        {
            for (int i = 0; i < onShow.Length; i++)
            {
                _onStartOfDialog.AddListener(onShow[i]);
            }
        }
    }

    private void ClosePanel()
    {
        _modalPanelObject.SetActive(false);
        _onEndOfDialog?.Invoke();
    }

    public enum PanelButtons
    {
        One,
        Two,
        Three,
    }
}