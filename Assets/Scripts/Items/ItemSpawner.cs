using UnityEngine;

namespace Items
{
    public class ItemSpawner : MonoBehaviour
    {
        [Header("Item type")] 
        [SerializeField] private ItemCategoryType itemType;
    }
}