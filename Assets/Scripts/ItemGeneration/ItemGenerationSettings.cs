using System.Collections.Generic;
using UnityEngine;

namespace ItemGeneration
{
    [CreateAssetMenu(menuName = "Items/Item Generation Settings")]
    public class ItemGenerationSettings : ScriptableObject
    {
        [Header("Generation")]
        [Min(0)] public int maxItemsPerRoom = 2;

        [Header("Items")]
        public List<ItemGenerationData> items;
    }
}