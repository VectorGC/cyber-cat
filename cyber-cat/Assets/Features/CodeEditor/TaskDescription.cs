using Shared.Models;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class TaskDescription : UIBehaviour
    {
        [SerializeField] private TMP_Text goalTask;
        [SerializeField] private TMP_Text descriptionTask;

        public ITask Task
        {
            set => SetTask(value);
        }

        private void SetTask(ITask task)
        {
            goalTask.text = task.Name;
            descriptionTask.text = task.Description;
        }
    }
}