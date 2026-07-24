using Assets.Scripts.Player.Orchestrator;
using UnityEngine;

namespace Movement
{
    public class PlayerMovement : MonoBehaviour, IInputHandler 
    {
        [Header("Movement settings")]
        [SerializeField] private float moveSpeed = 5f;

        private Rigidbody2D _rb;
        private Vector2 _input;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void HandleInput()
        {
            _input.x = Input.GetAxisRaw("Horizontal");
            _input.y = Input.GetAxisRaw("Vertical");
            _input = _input.normalized;

            if(_input.magnitude >= 0.01)
            {
                SoundManager.Instance.StartWalkingSound();
            }
            else
            {
                SoundManager.Instance.StopWalkingSound();
            }
        }

        private void FixedUpdate()
        {
            _rb.MovePosition(_rb.position + _input * (moveSpeed * Time.fixedDeltaTime));
        }
    }
}
