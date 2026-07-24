using System;
using Managers;
using UnityEngine;

namespace Items
{
    public class Item : MonoBehaviour, IInteractable
    {
        [field: SerializeField]
        public ItemData ItemData { get; private set; }
        
        [Header("Managers")]
        private StepManager _stepManager;
        private InventoryManager _inventoryManager;

        [Obsolete("Obsolete")]
        private void Awake()
        {
            _stepManager = FindFirstObjectByType<StepManager>();
            _inventoryManager = FindFirstObjectByType<InventoryManager>();
        }
        
        public void Interact()
        {
            switch (ItemData.categoryType)
            {
                case ItemCategoryType.Valuable:
                    _inventoryManager.AddValuable(ItemData);
                    gameObject.SetActive(false);
                    break;

                case ItemCategoryType.Consumable:
                    var oldItem = _inventoryManager.SetConsumable(ItemData);

                    if (oldItem != null)
                    {
                        // TODO:
                        // create old item instead old
                    }

                    gameObject.SetActive(false);
                    break;
                
                case ItemCategoryType.Upgrade:
                    ApplyEffects();
                    gameObject.SetActive(false);
                    break;
            }
        }

        private void ApplyEffects()
        {
            foreach (var effect in ItemData.effects)
            {
                switch (effect.effectType)
                {
                    case ItemEffectType.AddSteps:
                        Debug.Log("Adding steps");
                        _stepManager.AddSteps(effect.value);
                        break;
                    
                    case ItemEffectType.RestoreToMaxSteps:
                        Debug.Log("Restore to Max Steps");
                        _stepManager.RestoreToMaxSteps();
                        break;

                    case ItemEffectType.IncreaseMaxSteps:
                        Debug.Log("Increased Max Steps");
                        _stepManager.IncreaseMaxSteps(effect.value);
                        break;
                }
            }
        }
    }
}