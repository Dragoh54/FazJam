using Doors.SO;
using Managers;
using UnityEngine;

namespace Doors
{
    public class StoryDoor : Door
    {
        [Header("Requirements")]
        [SerializeField] private DoorRequirement requirement;
        
        private StoryProgressManager _storyProgressManager;
        
        protected override void Awake()
        {
            base.Awake();

            _storyProgressManager = FindFirstObjectByType<StoryProgressManager>();
        }
        
        public override void Interact()
        {
            if (!CanInteract())
                return;

            base.Interact();
        }
        
        protected bool CanInteract()
        {
            if (requirement == null)
                return true;

            if (_storyProgressManager.HasFlag(requirement.requiredFlag))
                return true;

            Debug.Log(requirement.lockedMessage);

            return false;
        }
    }
}