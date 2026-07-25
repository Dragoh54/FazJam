using Assets.Scripts.Player.Orchestrator;
using Items;
using UnityEngine;

namespace Player.Interactions
{
    public class PlayerInteraction : MonoBehaviour, IInputHandler
    {
        private IInteractable _currentInteractable;
        private InteractionPrompt _prompt;
        
        [Header("Settings")]
        [SerializeField] private float interactionRadius = 0.7f;
        [SerializeField] private LayerMask interactableLayer;

        private readonly Collider2D[] _results = new Collider2D[8];
        
        private void Awake()
        {
            _prompt = GetComponentInChildren<InteractionPrompt>();
        }
        
        private void Update()
        {
            FindInteractable();
        }

        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _currentInteractable?.Interact();
            }
        }

        private void FindInteractable()
        {
            var count = Physics2D.OverlapCircleNonAlloc(
                transform.position,
                interactionRadius,
                _results,
                interactableLayer);

            IInteractable closest = null;
            var closestDistance = float.MaxValue;

            for (var i = 0; i < count; i++)
            {
                if (!_results[i].TryGetComponent(out IInteractable interactable))
                    continue;

                var distance = Vector2.SqrMagnitude(
                    _results[i].transform.position - transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = interactable;
                }

                _results[i] = null;
            }

            if (closest != _currentInteractable)
            {
                _currentInteractable = closest;

                if (_currentInteractable != null)
                    _prompt.Show();
                else
                    _prompt.Hide();
            }
        }
    }
}