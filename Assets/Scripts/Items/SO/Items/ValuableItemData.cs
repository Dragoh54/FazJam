using Managers;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/Valuable")]
    public class ValuableItemData : ItemData
    {
        [Header("Valuable Item Data")]
        public int sellPrice;

        public override ItemCategoryType CategoryType => ItemCategoryType.Valuable;
        public override Color NotificationColor => Color.yellow;

        public override void OnPickup(Item item)
        {
            item.inventoryManager.AddValuable(this);

            item.PickedUp();
        }
    }
}