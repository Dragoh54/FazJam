using System.Collections.Generic;
using System.Linq;
using Items;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace Managers
{
    public class InventoryManager : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Image consumableIcon;
        [SerializeField] private Image consumablePrompt;
        
        [Header("Items")]
        private readonly List<ValuableItemData> _valuables = new();
        public ConsumableItemData ConsumeItem { get; private set; }

        private void Start()
        {
            UpdateConsumableUI();
        }

        //TODO: JUST FOR TESTING
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                PrintInventory();
            }
        }

        public void AddValuable(ValuableItemData item)
        {
            _valuables.Add(item);
            
            Debug.Log($"Added: {item.itemName}");
        }
        
        public ConsumableItemData SetConsumable(ConsumableItemData newItem)
        {
            var oldItem = ConsumeItem;

            ConsumeItem = newItem;

            Debug.Log($"Current consumable: {ConsumeItem.itemName}");

            UpdateConsumableUI();

            return oldItem;
        }
        
        public ConsumableItemData UseConsumable()
        {
            if (ConsumeItem == null)
                return null;

            var item = ConsumeItem;
            ConsumeItem = null;
            
            UpdateConsumableUI();

            return item;
        }
        
        private void UpdateConsumableUI()
        {
            if (ConsumeItem == null)
            {
                consumableIcon.sprite = null;
                consumablePrompt.enabled = false;
                consumableIcon.enabled = false; 
                return;
            }

            consumableIcon.sprite = ConsumeItem.icon;
            consumableIcon.enabled = true;
            consumablePrompt.enabled = true;
        }
        
        public void RemoveConsumable()
        {
            if (ConsumeItem == null)
                return;

            Debug.Log($"Consumable used: {ConsumeItem.itemName}");

            ConsumeItem = null;
        }
        
        private void PrintInventory()
        {
            Debug.Log("===== Valuables =====");

            foreach (var item in _valuables)
                Debug.Log($"{item.itemName} ({item.sellPrice})");

            Debug.Log($"Total: {GetTotalValue()}");
        }
        
        public int GetTotalValue()
        {
            return _valuables.Sum(item => item.sellPrice);
        }
    }
}