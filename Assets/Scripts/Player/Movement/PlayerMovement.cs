using Assets.Scripts.Player.Orchestrator;
using UnityEngine;

namespace Movement
{
    public class PlayerMovement : MonoBehaviour, IInputHandler 
    {
        [Header("Movement settings")]
        [SerializeField] private float moveSpeed = 5f;

        private Rigidbody2D _rb;
        private Animator _playerAnimator;
        private Vector2 _input;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _playerAnimator = GetComponent<Animator>();
        }

        public void HandleInput()
        {
            _input.x = Input.GetAxisRaw("Horizontal");
            _input.y = Input.GetAxisRaw("Vertical");
            _input = _input.normalized;

            if(_input.magnitude >= 0.01)
            {
                SoundManager.Instance.StartWalkingSound();

                _playerAnimator.SetFloat("Horizontal", _input.x);
                _playerAnimator.SetFloat("Vertical", _input.y);

            }
            else
            {
                SoundManager.Instance.StopWalkingSound();
            }

            if (Mathf.Abs(_input.y) > Mathf.Abs(_input.x))
            {
                _input.x = 0;
            }
            else
            {
                _input.y = 0;
            }

            _playerAnimator.SetFloat("Speed", _input.magnitude);
        }

        private void FixedUpdate()
        {
            _rb.MovePosition(_rb.position + _input * (moveSpeed * Time.fixedDeltaTime));
        }
    }
}
