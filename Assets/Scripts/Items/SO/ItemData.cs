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
        [FormerlySerializedAs("category")] public ItemCategoryType categoryType;
        
        [TextArea]
        public string description;

        [Header("Shop")] 
        public int price;
        
        [FormerlySerializedAs("spawnLocation")] [Header("Spawn")]
        public SpawnLocationType spawnLocationType;
        
        [Header("Effect")]
        public ItemEffectData[] effects; 
    }
}