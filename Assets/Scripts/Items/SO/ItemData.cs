using UnityEngine;
using UnityEngine.Serialization;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/Item")]
    public class ItemData : ScriptableObject
    {
        [Header("General")] 
        public string itemName;
        public Sprite icon;
        public ItemCategoryType categoryType;
        
        [TextArea]
        public string description;

        [Header("Shop")] 
        public int price;
        
        [Header("Spawn")]
        public GameObject itemPrefab;
        
        [Header("Effect")]
        public ItemEffectData[] effects; 
    }
}