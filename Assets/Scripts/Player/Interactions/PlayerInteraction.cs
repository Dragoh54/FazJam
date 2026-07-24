using Assets.Scripts.Player.Orchestrator;
using Items;
using UnityEngine;

namespace Player.Interactions
{
    public class PlayerInteraction : MonoBehaviour, IInputHandler
    {
        private IInteractable _currentInteractable;
        private InteractionPrompt _prompt;
        
        private void Awake()
        {
            _prompt = GetComponentInChildren<InteractionPrompt>();
        }
        
        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _currentInteractable?.Interact();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var interactable = other.GetComponent<IInteractable>();

            if (interactable != null)
            {
                _currentInteractable = interactable;
                _prompt.Show();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var interactable = other.GetComponent<IInteractable>();

            if (interactable != null && interactable == _currentInteractable)
            {
                _currentInteractable = null;
                _prompt.Hide();
            }
        }
    }
}