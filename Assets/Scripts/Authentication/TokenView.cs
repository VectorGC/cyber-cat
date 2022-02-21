using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;

namespace Authentication
{
    [RequireComponent(typeof(TMP_Text))]
    public class TokenView : MonoBehaviour
    {
        [SerializeField] private TMP_Text tokenText;

        [Conditional("DEBUG")]
        private void Start()
        {
            var token = TokenSession.FromPlayerPrefs();
            ShowToken(token);

            TokenSession.TokenSavedToPlayerPrefs += ShowToken;
        }

        private void ShowToken(TokenSession token)
        {
            tokenText.SetText(token.Token);
        }

        private void OnDestroy()
        {
            TokenSession.TokenSavedToPlayerPrefs -= ShowToken;
        }

        private void OnValidate()
        {
            TryGetComponent(out tokenText);
        }
    }
}