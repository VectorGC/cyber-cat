using Cysharp.Threading.Tasks;
using ServerAPI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class AuthorizationController : UIBehaviour
{
    protected override async void Start()
    {
        var client = ServerAPIFacade.Create();
        var token = await client.Authenticate("cat", "cat");
        client.AddAuthorizationToken(token);

        var playerName = await client.AuthorizePlayer(token);

        TokenRepository.Token = token;
        TokenRepository.PlayerName = playerName;

        await SceneManager.LoadSceneAsync("MainMenu").ToUniTask();
    }
}