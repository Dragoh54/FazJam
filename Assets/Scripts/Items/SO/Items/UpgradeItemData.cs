using Items.SO.Effects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/Upgrade")]
    public class UpgradeItemData : EffectItemData
    {
        [Header("Upgrade Item Data")] 
        public int price;

        public override ItemCategoryType CategoryType => ItemCategoryType.Upgrade;

        public override void OnPickup(Item item)
        {
            ApplyEffects(item.stepManager);
            item.PickedUp();

            item.PickedUp();
        }
    }
}