using Authentication;
using UnityEngine;

public class TokenDeleter : MonoBehaviour
{
    public void DeleteToken() => TokenSession.DeleteInPlayerPrefs();
}