using System;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Items
{
    public class Item : MonoBehaviour, IInteractable
    {
        [field: SerializeField]
        public ItemData ItemData { get; private set; }
        
        [Header("Managers")]
        public StepManager stepManager;
        public InventoryManager inventoryManager;
        public StoryProgressManager storyProgressManager;

        [Obsolete("Obsolete")]
        private void Awake()
        {
            stepManager = FindFirstObjectByType<StepManager>();
            inventoryManager = FindFirstObjectByType<InventoryManager>();
            storyProgressManager = FindFirstObjectByType<StoryProgressManager>();
        }
        
        public void Interact()
        {
            Debug.Log(ItemData);
            ItemData.OnPickup(this);
        }

        public void PickedUp()
        {
            gameObject.SetActive(false);
        }
    }
}