using Items;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    [SerializeField]
    private Item[] Consumables;

    [SerializeField]
    private Item[] Upgrades;

    public void HandleVisibility(bool hasConsumable, int CurrentMoney)
    {
        if (!hasConsumable)
        {
            foreach (var item in Consumables)
            {
                if (item.ItemData is ConsumableItemData data && data.price > CurrentMoney)
                {
                    item.transform.parent.gameObject.SetActive(false);
                }
                else
                {
                    item.transform.parent.gameObject.SetActive(true);
                }
            }
        }

        foreach (var item in Upgrades)
        {
            if (item.ItemData is UpgradeItemData data && data.price > CurrentMoney)
            {
                item.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                item.transform.parent.gameObject.SetActive(true);
            }
        }
    }
}
