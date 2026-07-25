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
        
        public abstract void OnPickup(Item item);
    }
}