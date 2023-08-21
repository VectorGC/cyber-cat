using Shared.Models.Dto;
using Shared.Models.Models;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class TaskDescription : UIBehaviour
    {
        [SerializeField] private TMP_Text goalTask;
        [SerializeField] private TMP_Text descriptionTask;

        public TaskDto Task
        {
            set => SetTask(value);
        }

        private void SetTask(TaskDto task)
        {
            goalTask.text = task.Name;
            descriptionTask.text = task.Description;
        }
    }
}