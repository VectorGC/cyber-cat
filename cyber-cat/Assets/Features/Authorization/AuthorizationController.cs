using ApiGateway.Client;
using Cysharp.Threading.Tasks;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class AuthorizationController : UIBehaviour
{
    protected override async void Start()
    {
        var authorizationService = AuthorizationServiceFactory.Create(GameManager.ServerUri);
        var token = await authorizationService.Authenticate("cat", "cat");
        var playerName = await authorizationService.AuthorizePlayer(token);

        TokenRepository.Token = token;
        TokenRepository.PlayerName = playerName;

        await SceneManager.LoadSceneAsync("MainMenu").ToUniTask();
    }
}