using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Rooms
{
    public class RoomSpawnPoints : MonoBehaviour
    {
        [Header("Item spawning")]
        [SerializeField] private List<ItemSpawnPoint> spawnPoints;
        public IReadOnlyList<ItemSpawnPoint> SpawnPoints => spawnPoints;
        public int SpawnedItems { get; private set; }
        
        public bool CanSpawn(int maxItems)
        {
            return SpawnedItems < maxItems;
        }


        public void RegisterSpawn()
        {
            SpawnedItems++;
        }
        
        public void ClearSpawnedItems()
        {
            SpawnedItems = 0;

            foreach (var spawnPoint in spawnPoints)
            {
                spawnPoint.Clear();
            }
        }
    }
}