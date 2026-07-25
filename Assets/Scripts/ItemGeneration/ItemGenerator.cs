using System.Collections.Generic;
using System.Linq;
using Items;
using Rooms;
using UnityEngine;

namespace ItemGeneration
{
    public class ItemGenerator : MonoBehaviour
    {
        [SerializeField] private ItemGenerationSettings generationSettings;

        private List<RoomSpawnPoints> _rooms;
        
        private void Start()
        {
            _rooms = FindObjectsByType<RoomSpawnPoints>(FindObjectsSortMode.None).ToList();

            GenerateItems();
        }
        
        private void GenerateItems()
        {
            foreach (var itemData in generationSettings.items)
            {
                SpawnItemAmount(itemData);
            }
        }
        
        private void SpawnItemAmount(ItemGenerationData itemData)
        {
            for (var i = 0; i < itemData.maxCount; i++)
            {
                var spawnPoint = FindSpawnPoint(itemData.item.CategoryType);

                if (spawnPoint == null)
                {
                    Debug.Log(
                        $"No more place {itemData.item.itemName}"
                    );

                    return;
                }


                Instantiate(
                    itemData.item.itemPrefab,
                    spawnPoint.transform.position,
                    Quaternion.identity
                );


                spawnPoint.Occupy();

                spawnPoint.RoomSpawnPoints.RegisterSpawn();
            }
        }
        
        private ItemSpawnPoint FindSpawnPoint(ItemCategoryType category)
        {
            List<ItemSpawnPoint> possiblePoints = new();


            foreach (var room in _rooms)
            {
                if (!room.CanSpawn(generationSettings.maxItemsPerRoom))
                    continue;


                foreach (var point in room.SpawnPoints)
                {
                    if (!point.IsOccupied &&
                        point.CanSpawn(category))
                    {
                        possiblePoints.Add(point);
                    }
                }
            }


            if (possiblePoints.Count == 0)
                return null;


            return possiblePoints[
                Random.Range(0, possiblePoints.Count)
            ];
        }
    }
}