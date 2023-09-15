using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private HUDController _hud;

    public void InstallBindings(DiContainer container)
    {
        container.Bind<Interactable[]>().FromMethod(FindObjectsOfType<Interactable>).AsSingle();
        container.BindInstance(_player).AsSingle();
        container.Bind<IHud>().FromInstance(_hud).AsSingle();
        container.Bind<PlayerInteractHandler>().AsSingle();
    }
}