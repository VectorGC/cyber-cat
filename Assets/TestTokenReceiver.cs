using Authentication;
using UnityEngine;

public class TestTokenReceiver : MonoBehaviour
{
    private void Awake()
    {
        TokenSession.ReceiveFromServer(t => t.SaveToPlayerPrefs());
    }
}