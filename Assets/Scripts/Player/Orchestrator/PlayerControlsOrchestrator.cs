using Assets.Scripts.Player.Orchestrator;
using UnityEngine;

public class PlayerControlsOrchestrator : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private IInputHandler[] _playerInputHandlers;

    [SerializeField]
    private Sprite _playerIconOnZoomOut;

    private Sprite _prevSprite;

    public bool IsBlocked { get; set; }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _playerInputHandlers = GetComponents<IInputHandler>();
    }

    private void Update()
    {
        if (IsBlocked)
        {
            if (!_prevSprite)
            {
                _prevSprite = _spriteRenderer.sprite;
                _spriteRenderer.sprite = _playerIconOnZoomOut;
                _animator.enabled = false;
            }

            return;
        }

        if (!IsBlocked && _prevSprite)
        {
            _spriteRenderer.sprite = _prevSprite;
            _prevSprite = null;
            _animator.enabled = true;
        }

        foreach (var inputHandler in _playerInputHandlers)
        {
            inputHandler.HandleInput();
        }
    }
}
