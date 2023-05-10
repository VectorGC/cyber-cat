using Authentication;
using RestAPIWrapper;
using RestAPIWrapper.V1;
using System;
using TaskUnits;
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

        [SerializeField] private bool selfTriggerLogic = false;

        void Start()
        {
            _collider = GetComponent<SphereCollider>();
            _collider.OnCollisionStayAsObservable().Subscribe(x => Debug.Log("123"));
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            var isHackModePressed = Input.GetKey(KeyCode.F);
            if (isHackModePressed && GameMode.Vision == VisionMode.HackVision)
            {
                TokenSession token = PlayerPrefsInfo.GetToken();
            }
        }
    }
}