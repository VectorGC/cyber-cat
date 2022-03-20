using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeConsole: MonoBehaviour
{   
    //static field with Output text
    public  Text consoleText;
    private static CodeConsole Instance => FindObjectOfType<CodeConsole>();

    public void InnerSetBaseColor()
    {
        consoleText.color = Color.white;
    }

    public static void SetBaseColor()
    {
        Instance.InnerSetBaseColor();
    }
    public void InnerWriteLine(string msg)
    {
        consoleText.text = msg;
    }
    public void InnerWriteLine(string msg, MessageType type)
    {
        if (type == MessageType.Error)
            consoleText.color = Color.red;
        if (type == MessageType.Allert)
            consoleText.color = Color.yellow;
        consoleText.text = msg;
    }

    public static void WriteLine(string msg, MessageType type)
    {
        Instance.InnerWriteLine(msg, type);
    }

    public static void WriteLine(string msg)
    {
        Instance.InnerWriteLine(msg);
    }
    
    public void InnerClear()
    {
        consoleText.text = "";
    }

    public static void Clear()
    {
        Instance.InnerClear();
    }
}

//MessageType Enum
public enum MessageType
{
    Error, //0
    Allert //1
}