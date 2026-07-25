using Items;
using Managers;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private InventoryManager _inventoryManager;

    [SerializeField]
    private UIManager _uiManager;

    [field:SerializeField]
    public int CurrentMoney { get; private set; }
    [field: SerializeField]
    public int CurrentMoneyChange { get; private set; }

    private void Awake()
    {
        var shop = FindAnyObjectByType<Shop>();

        shop.OnShopOpened += HandleShopOpened;
    }

    private void HandleShopOpened()
    {
        int previousMoney = CurrentMoney;
        CurrentMoney += CurrentMoneyChange;

        CurrentMoneyChange = 0;

        _uiManager.HandleShopChange(_inventoryManager.ConsumeItem is not null, CurrentMoney);

        _uiManager.AnimateMoneyCounter(previousMoney, CurrentMoney);
    }

    public void AddMoneyChange(int money)
    {
        var prevMoneyChange = CurrentMoneyChange;
        CurrentMoneyChange += money;

        _uiManager.AnimateMoneyChange(prevMoneyChange, CurrentMoneyChange);
    }

    public void AddConsumableInInventory(Item item)
    {
        Debug.Log("item" + item.name);

        if(item.ItemData is ConsumableItemData data && CurrentMoney >= data.price && _inventoryManager.ConsumeItem is null)
        {
            _inventoryManager.SetConsumable(data);

            CurrentMoney -= data.price;

            _uiManager.UpdateMoneyCounter(CurrentMoney);

            _uiManager.HandleShopChange(_inventoryManager.ConsumeItem is not null, CurrentMoney);
        }
    }

    public void BuyUpgrades(Item item)
    {
        Debug.Log("upgrade" + item.name);

        if (item.ItemData is UpgradeItemData data && CurrentMoney >= data.price)
        {
            data.OnPickup(item);

            CurrentMoney -= data.price;

            _uiManager.UpdateMoneyCounter(CurrentMoney);

            _uiManager.HandleShopChange(_inventoryManager.ConsumeItem is not null, CurrentMoney);
        }
    }
}
