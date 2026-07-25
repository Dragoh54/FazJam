using Story;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/Story")]
    public class StoryItemData : ItemData
    {
        [Header("Story Item Data")]
        public StoryFlag storyFlag;
        public bool returnOnDeath;
        public override ItemCategoryType CategoryType => ItemCategoryType.Story;

        public override void OnPickup(Item item)
        {
            item.storyProgressManager.AddFlag(storyFlag);

            item.PickedUp();
        }
    }
}