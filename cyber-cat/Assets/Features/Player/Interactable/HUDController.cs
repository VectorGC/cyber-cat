using System;
using System.Collections.Generic;
using ApiGateway.Client.Application;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public interface IHud
{
    string HintText { get; set; }
}

public class HUDController : UIBehaviour, IHud
{
    [SerializeField] private Text _hintText;
    [SerializeField] private Button _settings;
    [SerializeField] private List<Image> _inventoryItems;

    public string HintText
    {
        get => _hintText.text;
        set
        {
            _hintTextOnDelay = value;
            _delay = 0.1f;
        }
    }

    private string _hintTextOnDelay;
    private float _delay;
    private PlayerInventory _playerInventory;
    private AuthorizationPresenter _authorizationPresenter;
    private ApiGatewayClient _client;

    [Inject]
    public void Construct(PlayerInventory playerInventory, AuthorizationPresenter authorizationPresenter, ApiGatewayClient client)
    {
        _client = client;
        _authorizationPresenter = authorizationPresenter;
        _playerInventory = playerInventory;
        _settings.onClick.AddListener(OnSettingsClick);
    }

    protected override void OnDestroy()
    {
        _settings.onClick.RemoveListener(OnSettingsClick);
    }

    private void OnSettingsClick()
    {
        if (_client.Player == null)
        {
            SimpleModalWindow.Create()
                .SetHeader("Настройки")
                .SetBody("Доступ ограничен. Ваш прогресс будет утерян. Авторизуйтесь в главном меню, чтобы сохранить.")
                .AddButton("Продолжить")
                // .AddButton("Авторизоваться", () => _authorizationPresenter.Show().Forget())
                .AddButton("Выйти в главное меню", () => SceneManager.LoadSceneAsync("MainMenu"))
                .Show();
        }
        else
        {
            SimpleModalWindow.Create()
                .SetHeader("Настройки")
                .SetBody($"Доступ получен: {_client.Player.User.FirstName}. Ваш прогресс будет сохранен.")
                .AddButton("Продолжить")
                .AddButton("Выйти в главное меню", () => SceneManager.LoadSceneAsync("MainMenu"))
                .Show();
        }
    }

    private void Update()
    {
        for (var i = 0; i < _inventoryItems.Count; i++)
        {
            var color = _inventoryItems[i].color;
            color.a = _playerInventory.Has((InventoryItem) i) ? 1 : 0;
            _inventoryItems[i].color = color;
        }
    }

    private void LateUpdate()
    {
        if (_delay > 0)
        {
            _delay -= Time.deltaTime;
            if (_hintTextOnDelay != _hintText.text)
            {
                _hintText.text = _hintTextOnDelay;
            }
        }
        else
        {
            _hintText.text = string.Empty;
        }

        _hintText.gameObject.SetActive(!string.IsNullOrEmpty(_hintText.text));
    }
}