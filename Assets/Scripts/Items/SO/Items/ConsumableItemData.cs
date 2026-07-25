using Items.SO.Effects;
using Managers;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/Consumable")]
    public class ConsumableItemData : EffectItemData
    {
        [Header("Consumable Item Data")] 
        public int price;

        public override ItemCategoryType CategoryType => ItemCategoryType.Consumable;
        public override Color NotificationColor => Color.green;

        public override void OnPickup(Item item)
        {
            var oldItem = item.inventoryManager.SetConsumable(this);

            if (oldItem != null)
            {
                SpawnOldItem(oldItem, item.transform.position);
            }

            item.PickedUp();
        }
        
        public void Use(StepManager stepManager)
        {
            ApplyEffects(stepManager);
        }
        
        private void SpawnOldItem(ConsumableItemData oldItem, Vector3 position)
        {
            Instantiate(
                oldItem.itemPrefab,
                position,
                Quaternion.identity
            );
        }
    }
}