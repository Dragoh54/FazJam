using ItemGeneration;
using UnityEngine;

namespace Items
{
    public class ItemSpawnPoint : MonoBehaviour
    {
        [Header("Spawn point type")]
        [SerializeField] private SpawnPointType allowedTypes;

        public SpawnPointType AllowedTypes => allowedTypes;

        public bool IsOccupied { get; private set; }

        public bool CanSpawn(ItemCategoryType category)
        {
            var requiredType = category switch
            {
                ItemCategoryType.Valuable => SpawnPointType.Valuable,

                ItemCategoryType.Consumable => SpawnPointType.Item,

                _ => SpawnPointType.None
            };

            return (allowedTypes & requiredType) != 0;
        }
    }
}