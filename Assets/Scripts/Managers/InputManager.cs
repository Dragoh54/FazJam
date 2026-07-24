using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void MapInteraction();
    public event MapInteraction OnMapOpened;
    public event MapInteraction OnMapClosed;

    public delegate void InstructructionInteraction();
    public event InstructructionInteraction OnInstructionOpened;
    public event InstructructionInteraction OnInstructionClosed;

    [SerializeField]
    private PlayerControlsOrchestrator _orchestrator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OnMapOpened?.Invoke();

            BlockInput();
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            OnMapClosed?.Invoke();

            EnableInput();
        } 
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            OnInstructionOpened?.Invoke();

            BlockInput();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            OnInstructionClosed?.Invoke();

            EnableInput();
        }
    }

    private void BlockInput()
        {
        if (_orchestrator)
        {
            _orchestrator.IsBlocked = true;
        }
    }

    private void EnableInput()
    {
        if (_orchestrator)
        {
            _orchestrator.IsBlocked = false;
        }
    }
}
