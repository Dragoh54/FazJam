using Items.SO.Effects;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/Consumable")]
    public class ConsumableItemData : EffectItemData
    {
        [Header("Consumable Item Data")] 
        public int price;

        public override ItemCategoryType CategoryType => ItemCategoryType.Consumable;

        public override void OnPickup(Item item)
        {
            var oldItem = item.inventoryManager.SetConsumable(this);

            if (oldItem != null)
            {
                // TODO: выбросить старый предмет
                // TODO: сделать в конце
            }

            item.PickedUp();
        }
        
        public void Use(StepManager stepManager)
        {
            ApplyEffects(stepManager);
        }
    }
}