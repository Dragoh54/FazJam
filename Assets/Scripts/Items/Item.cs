using System;
using UnityEngine;

namespace Items
{
    public class Item : MonoBehaviour, IInteractable
    {
        [SerializeField] private ItemData itemData;
        
        [Header("Step Manager")]
        private StepManager _stepManager;
        
        //todo: add sellManager

        [Obsolete("Obsolete")]
        private void Awake()
        {
            _stepManager = FindFirstObjectByType<StepManager>();
        }
        
        public void Interact()
        {
            switch (itemData.categoryType)
            {
                case ItemCategoryType.Valuable:
                    // sellManager.Add(itemData);
                    Debug.Log("Fur fur fur");
                    break;

                case ItemCategoryType.Consumable:
                case ItemCategoryType.Upgrade:
                    ApplyEffects();
                    break;
            }

            gameObject.SetActive(false);
        }

        private void ApplyEffects()
        {
            foreach (var effect in itemData.effects)
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