using ItemGeneration;
using Items;
using Managers;
using Sounds;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private InventoryManager _inventoryManager;

    [SerializeField]
    private ItemGenerator _itemGenerator;

    [SerializeField]
    private UIManager _uiManager;


    [SerializeField] private SoundData _moneySound;

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
        _itemGenerator.RegenerateItems();

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
            PlaySound();

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
            PlaySound();

            data.OnPickup(item);

            CurrentMoney -= data.price;

            _uiManager.UpdateMoneyCounter(CurrentMoney);

            _uiManager.HandleShopChange(_inventoryManager.ConsumeItem is not null, CurrentMoney);
        }
    }

    private void PlaySound()
    {
        SoundManager.Instance.PlaySFX(_moneySound);
    }
}
