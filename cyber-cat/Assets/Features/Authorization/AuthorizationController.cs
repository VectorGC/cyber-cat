using Cysharp.Threading.Tasks;
using ServerAPI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class AuthorizationController : UIBehaviour
{
    protected override async void Start()
    {
        var client = ServerEnvironment.CreateAnonymousClient();
        var token = await client.Authorization.GetAuthenticationToken("cat", "cat");

        TokenRepository.Token = token;
        TokenRepository.PlayerName = "Cat";

        await SceneManager.LoadSceneAsync("MainMenu").ToUniTask();
    }
}