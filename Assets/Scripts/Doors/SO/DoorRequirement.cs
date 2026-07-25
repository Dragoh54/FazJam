using Story;
using UnityEngine;

namespace Doors.SO
{
    [CreateAssetMenu(menuName = "Doors/Door Requirement")]
    public class DoorRequirement : ScriptableObject
    {
        public StoryFlag requiredFlag;
        public string lockedMessage;
    }
}