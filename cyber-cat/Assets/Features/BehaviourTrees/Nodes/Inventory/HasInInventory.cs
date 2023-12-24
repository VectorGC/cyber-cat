using Bonsai;
using Bonsai.Core;
using UnityEngine;
using Zenject;

[BonsaiNode("Inventory/")]
public class HasInInventory : ConditionalAbort
{
    [SerializeField] private InventoryItem _item;

    private PlayerInventory _playerInventory;

    [Inject]
    public void Construct(PlayerInventory playerInventory)
    {
        _playerInventory = playerInventory;
    }

    public override bool Condition()
    {
        return _playerInventory.Has(_item);
    }
}