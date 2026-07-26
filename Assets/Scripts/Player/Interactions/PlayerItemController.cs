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
            
            SoundManager.Instance.PlaySFX(item.useSound);

            item.Use(_stepManager);
        }
    }
}