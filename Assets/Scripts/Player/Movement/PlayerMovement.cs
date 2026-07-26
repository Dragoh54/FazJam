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

            if (_input.magnitude >= 0.01f)
            {
                SoundManager.Instance.StartWalkingSound();
                
                if (Mathf.Abs(_input.x) > Mathf.Abs(_input.y))
                {
                    _playerAnimator.SetFloat("Horizontal", Mathf.Sign(_input.x));
                    _playerAnimator.SetFloat("Vertical", 0f);
                }
                else
                {
                    _playerAnimator.SetFloat("Horizontal", 0f);
                    _playerAnimator.SetFloat("Vertical", Mathf.Sign(_input.y));
                }
            }
            else
            {
                SoundManager.Instance.StopWalkingSound();
            }

            _playerAnimator.SetFloat("Speed", _input.magnitude);
        }

        private void FixedUpdate()
        {
            _rb.MovePosition(
                _rb.position + _input * (moveSpeed * Time.fixedDeltaTime));
            _input = Vector2.zero;
            SoundManager.Instance.StopWalkingSound();
        }
    }
}
