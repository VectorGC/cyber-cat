using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Legacy_do_not_use_it
{
    [Obsolete]
    public class InteractCrystal : MonoBehaviour
    {
        private SphereCollider _collider;

        [SerializeField] private TaskUnitFolder taskFolder;

        // Start is called before the first frame update
        void Start()
        {
            _collider = GetComponent<SphereCollider>();
            _collider.OnCollisionStayAsObservable().Subscribe(x => Debug.Log("123"));
        }

        private async void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            var isHackModePressed = Input.GetKey(KeyCode.F);
            if (isHackModePressed && GameMode.Vision == VisionMode.HackVision)
            {
                CodeEditor.OpenSolution(taskFolder).Forget();
            }
        }
    }
}