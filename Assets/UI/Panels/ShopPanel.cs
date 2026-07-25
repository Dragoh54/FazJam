using Items;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    [SerializeField]
    private ShopItem[] Consumables;

    [SerializeField]
    private ShopItem[] Upgrades;

    public void HandleVisibility(bool hasConsumable, int CurrentMoney)
    {
        if (!hasConsumable)
        {
            foreach (var item in Consumables)
            {
                if (item.Item.ItemData is ConsumableItemData data && data.price > CurrentMoney)
                {
                    item.Disable();
                }
                else
                {
                    item.Enable();
                }
            }
        }

        foreach (var item in Upgrades)
        {
            if (item.Item.ItemData is UpgradeItemData data && data.price > CurrentMoney)
            {
                item.Disable();
            }
            else
            {
                item.Enable();
            }
        }
    }
}
