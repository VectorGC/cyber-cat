using Authentication;
using UnityEngine;

public class TestTokenReceiver : MonoBehaviour
{
    private void Awake()
    {
        TokenSession.ReceiveFromServer("test123@gmail.com", "123456qwer");
    }
}