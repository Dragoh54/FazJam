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
        
        private PickupNotificationManager _pickupNotificationManager;

        [Obsolete("Obsolete")]
        private void Awake()
        {
            stepManager = FindFirstObjectByType<StepManager>();
            inventoryManager = FindFirstObjectByType<InventoryManager>();
            storyProgressManager = FindFirstObjectByType<StoryProgressManager>();
            
            _pickupNotificationManager = FindFirstObjectByType<PickupNotificationManager>();
        }
        
        public void Interact()
        {
            if (ItemData.pickupSound != null)
            {
                SoundManager.Instance.PlaySFX(ItemData.pickupSound);
            }
            
            ShowPickupNotification();
            
            ItemData.OnPickup(this);
        }

        public void PickedUp()
        {
            gameObject.SetActive(false);
        }

        private void ShowPickupNotification()
        {
            _pickupNotificationManager.AddMessage(
                $"Collected {ItemData.itemName}",
                ItemData.NotificationColor);

            if (ItemData is StoryItemData story)
            {
                _pickupNotificationManager.AddMessage(
                    story.description,
                    Color.red);
            }
        }
    }
}