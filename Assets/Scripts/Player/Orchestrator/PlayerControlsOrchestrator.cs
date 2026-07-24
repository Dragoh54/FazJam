using Assets.Scripts.Player.Orchestrator;
using UnityEngine;

public class PlayerControlsOrchestrator : MonoBehaviour
{
    private IInputHandler[] _playerInputHandlers;

    public bool IsBlocked { get; set; }

    private void Awake()
    {
        _playerInputHandlers = GetComponents<IInputHandler>();
    }

    private void Update()
    {
        if (!IsBlocked)
        {
            foreach (var inputHandler in _playerInputHandlers)
            {
                inputHandler.HandleInput();
            }
        }
    }
}
