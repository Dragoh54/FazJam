using System.Collections.Generic;
using System.Linq;
using Items;
using Microsoft.Unity.VisualStudio.Editor;
using NUnit.Framework;
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
        private readonly List<ItemData> _valuables = new();
        private ItemData _consumeItem;

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

        public void AddValuable(ItemData item)
        {
            _valuables.Add(item);
            
            Debug.Log($"Added: {item.itemName}");
        }
        
        public ItemData SetConsumable(ItemData newItem)
        {
            var oldItem = _consumeItem;

            _consumeItem = newItem;

            Debug.Log($"Consumable: {_consumeItem.itemName}");
            
            UpdateConsumableUI();

            return oldItem;
        }
        
        public ItemData UseConsumable()
        {
            if (_consumeItem == null)
                return null;

            var item = _consumeItem;
            _consumeItem = null;
            
            UpdateConsumableUI();

            return item;
        }
        
        private void UpdateConsumableUI()
        {
            if (_consumeItem == null)
            {
                consumableIcon.sprite = null;
                consumablePrompt.enabled = false;
                consumableIcon.enabled = false; 
                return;
            }

            consumableIcon.sprite = _consumeItem.icon;
            consumableIcon.enabled = true;
            consumablePrompt.enabled = true;
        }
        
        public void RemoveConsumable()
        {
            if (_consumeItem == null)
                return;

            Debug.Log($"Consumable used: {_consumeItem.itemName}");

            _consumeItem = null;
        }
        
        private void PrintInventory()
        {
            Debug.Log("===== Valuables =====");

            foreach (var item in _valuables)
                Debug.Log($"{item.itemName} ({item.price})");

            Debug.Log($"Total: {GetTotalValue()}");
        }
        
        public int GetTotalValue()
        {
            return _valuables.Sum(item => item.price);
        }
    }
}