using Sounds;
using UnityEngine;
using UnityEngine.Serialization;

namespace Items
{
    public abstract class ItemData : ScriptableObject
    {
        [Header("Item Data")]
        public string itemName;
        public Sprite icon;
        public string description;

        public abstract ItemCategoryType CategoryType { get; }

        [Header("Spawn")]
        public GameObject itemPrefab;
        
        [Header("Notification color")]
        public virtual Color NotificationColor => Color.white;
        
        [Header("Audio")]
        public SoundData pickupSound;
        
        public abstract void OnPickup(Item item);
    }
}