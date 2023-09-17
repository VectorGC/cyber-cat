using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace mikinel.easylogview
{
    public class EasyLogView : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private bool isAutoScroll = true;
        [SerializeField] private int maxLines = 30;

        private void Awake()
        {
            if (text == null)
            {
                Debug.LogError($"Please attach TextMeshProUGUI");
                this.enabled = false;
                return;
            }

            if (scrollRect == null)
            {
                Debug.LogError($"Please attach ScrollView");
                this.enabled = false;
                return;
            }
        }

        private void Start()
        {
            text.text = string.Empty;
        }

        public void LogMessage(string logText, LogMessageType type, bool printTime)
        {
            if (text == null)
                return;

            var tmp = text.text;
            TrimLine(ref tmp, maxLines);
            text.text = tmp;

            var hashcode = GetColorHashCode(type);
            if (printTime)
            {
                PrintMessageWithTime(logText, hashcode);
            }
            else
            {
                PrintMessage(logText, hashcode);
            }

            if (isAutoScroll)
            {
                scrollRect.verticalNormalizedPosition = 0;
                scrollRect.horizontalNormalizedPosition = 0;
            }
        }

        private void PrintMessageWithTime(string logText, string hashcode)
        {
            var seconds = $"{DateTime.Now:hh:mm:ss}";
            if (string.IsNullOrEmpty(hashcode))
            {
                text.text = $"[{seconds}] {logText} \n" + text.text;
                return;
            }

            text.text = $"<color=#{hashcode}>[{seconds}] {logText}</color> \n" + text.text;
        }
        
        private void PrintMessage(string logText, string hashcode)
        {
            if (string.IsNullOrEmpty(hashcode))
            {
                text.text = $"{logText} \n" + text.text;
                return;
            }

            text.text = $"<color=#{hashcode}>{logText}</color> \n" + text.text;
        }

        private string GetColorHashCode(LogMessageType type)
        {
            switch (type)
            {
                case LogMessageType.Warning:
                    return "ffff00";
                case LogMessageType.Error:
                    return "ff0000";
                case LogMessageType.Success:
                    return "10FD1F";
                default:
                    return string.Empty;
            }
        }
        
        public void ClearLog()
        {
            text.text = "Clear Log \n";
        }

        private int CountChar(string s, char c)
        {
            return s.Length - s.Replace(c.ToString(), "").Length;
        }

        private void TrimLine(ref string s, int maxLine)
        {
            if (CountChar(s, '\n') >= maxLine)
                s = RemoveFirstLine(s);

            if (CountChar(s, '\n') >= maxLine)
                TrimLine(ref s, maxLine);
        }

        private string RemoveFirstLine(string s)
        {
            var pos = s.IndexOf('\n');
            return s.Substring(pos + 1, s.Length - pos - 1);
        }
    }
}