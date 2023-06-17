using System;
using UnityEngine;

namespace Features.QuestSystem.Actions
{
    [Serializable]
    public class ShowSimpleModal : IAction
    {
        [SerializeField] private string _header;
        [SerializeField] private string _body;

        public void Execute()
        {
            SimpleModalWindow.Create()
                .SetHeader(_header)
                .SetBody(_body)
                .Show();
        }
    }
}