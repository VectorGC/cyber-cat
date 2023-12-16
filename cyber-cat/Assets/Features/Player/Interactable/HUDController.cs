using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public interface IHud
{
    string HintText { get; set; }
}

public class HUDController : UIBehaviour, IHud
{
    [SerializeField] private Text _hintText;
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

    [Inject]
    public void Construct(PlayerInventory playerInventory)
    {
        _playerInventory = playerInventory;
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