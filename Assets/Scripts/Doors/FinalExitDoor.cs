using UnityEngine;

namespace Doors
{
    public class FinalExitDoor : StoryDoor
    {
        public override void Interact()
        {
            if (!CanInteract())
                return;

            //TODO: WIN
            Debug.Log("Win!");
        }
    }
}