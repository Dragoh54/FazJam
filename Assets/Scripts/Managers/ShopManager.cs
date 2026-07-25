using Items;
using Managers;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private InventoryManager _inventoryManager;

    [SerializeField]
    private UIManager _uiManager;

    public int CurrentMoney { get; private set; }
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

        _inventoryManager.SetConsumable(item.ItemData);
    }

    public void BuyUpgrades(Item item)
    {
        Debug.Log("upgrade" + item.name);

        item.Interact();
    }
}
