using Items;
using Managers;
using UnityEngine;

namespace Player.Interactions
{
    public class PlayerItemController : MonoBehaviour
    {
        private InventoryManager _inventoryManager;
        private StepManager _stepManager;

        private void Awake()
        {
            _stepManager = FindAnyObjectByType<StepManager>();
            _inventoryManager = FindAnyObjectByType<InventoryManager>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                UseConsumable();
            }
        }

        private void UseConsumable()
        {
            var item = _inventoryManager.UseConsumable();

            if (item == null)
                return;

            foreach (var effect in item.effects)
            {
                switch (effect.effectType)
                {
                    case ItemEffectType.AddSteps:
                        _stepManager.AddSteps(effect.value);
                        break;

                    case ItemEffectType.RestoreToMaxSteps:
                        _stepManager.RestoreToMaxSteps();
                        break;

                    case ItemEffectType.IncreaseMaxSteps:
                        _stepManager.IncreaseMaxSteps(effect.value);
                        break;
                }
            }

            Debug.Log($"Used {item.itemName}");
        }
    }
}