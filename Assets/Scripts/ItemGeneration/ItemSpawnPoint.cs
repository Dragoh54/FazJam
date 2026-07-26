using ItemGeneration;
using Rooms;
using UnityEngine;

namespace Items
{
    public class ItemSpawnPoint : MonoBehaviour
    {
        [Header("Spawn point type")]
        [SerializeField] private SpawnPointType allowedTypes;
        public SpawnPointType AllowedTypes => allowedTypes;

        public bool IsOccupied { get; private set; }
        
        public RoomSpawnPoints RoomSpawnPoints { get; private set; }
        
        private void Awake()
        {
            RoomSpawnPoints = GetComponentInParent<RoomSpawnPoints>();
        }
        
        public void Occupy()
        {
            IsOccupied = true;
        }

        public bool CanSpawn(ItemCategoryType category)
        {
            var requiredType = category switch
            {
                ItemCategoryType.Valuable => SpawnPointType.Valuable,
                ItemCategoryType.Consumable => SpawnPointType.Consumable,
                _ => SpawnPointType.None
            };

            return (allowedTypes & requiredType) != 0;
        }
        
        public void Clear()
        {
            IsOccupied = false;

            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}