using System;
using UnityEngine;
using UnityEventTypes;

namespace Authentication
{
    public class OnTokenReceived : MonoBehaviour
    {
        [SerializeField] private UnityStringEvent OnTokenSaved;

        private void Start()
        {
            TokenSession.TokenSavedToPlayerPrefs += CallTokenSaved;
        }

        private void CallTokenSaved(TokenSession token)
        {
            OnTokenSaved?.Invoke(token.Token);
        }

        private void OnDestroy()
        {
            TokenSession.TokenSavedToPlayerPrefs -= CallTokenSaved;
        }
    }
}