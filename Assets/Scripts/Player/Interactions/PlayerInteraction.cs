using Items;
using Microsoft.Extensions.Logging.Abstractions;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player.Interactions
{
    public class PlayerInteraction : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float interactionRadius = 0.5f;
        [SerializeField] private LayerMask interactableLayer;
        
        private IInteractable _currentInteractable;
        private HighlightEffect _currentHighlight;
        
        private void Update()
        {
            FindInteractable();

            if (Input.GetKeyDown(KeyCode.E))
            {
                _currentInteractable?.Interact();
            }
        }
        
        private void FindInteractable()
        {
            var hit = Physics2D.OverlapCircle(
                transform.position,
                interactionRadius,
                interactableLayer
                );

            IInteractable  newInteractable = null;
            HighlightEffect newHighlight = null;
            

            if (hit != null)
            {
                newInteractable = hit.GetComponent<IInteractable>();
                newHighlight = hit.GetComponent<HighlightEffect>();
            }
            
            if (_currentInteractable != newInteractable)
            {
                _currentHighlight?.DisableHighlight();
                
                _currentInteractable = newInteractable;
                _currentHighlight = newHighlight;
                
                _currentHighlight?.EnableHighlight();
            }
        }
    }
}