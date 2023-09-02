using ApiGateway.Client.Models;
using Cysharp.Threading.Tasks;
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
            StartCoroutine(task.GetName().AsUniTask().ContinueWith(name => goalTask.text = name).ToCoroutine());
            StartCoroutine(task.GetDescription().AsUniTask().ContinueWith(description => descriptionTask.text = description).ToCoroutine());
        }
    }
}