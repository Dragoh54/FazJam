namespace Doors
{
    public class FinalExitDoor : StoryDoor
    {
        public delegate void Escaped();
        public event Escaped OnEscaped;

        public override void Interact()
        {
            if (!CanInteract())
                return;

            OnEscaped?.Invoke();
        }
    }
}