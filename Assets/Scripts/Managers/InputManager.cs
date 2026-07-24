using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void MapInteraction();
    public event MapInteraction OnMapOpened;
    public event MapInteraction OnMapClosed;

    public delegate void InstructionInteraction();
    public event InstructionInteraction OnInstructionOpened;
    public event InstructionInteraction OnInstructionClosed;

    [SerializeField]
    private PlayerControlsOrchestrator _orchestrator;

    private bool _isBlocked = false;

    private void Awake()
    {
        var shop = FindAnyObjectByType<Shop>();
        var uiManager = FindAnyObjectByType<UIManager>();

        shop.OnShopOpened += BlockAllInput;
        uiManager.OnShopClosed += EnableAllInput;
    }

    private void Update()
    {
        if (_isBlocked)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OnMapOpened?.Invoke();

            BlockPlayerInput();
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            OnMapClosed?.Invoke();

            EnablePlayerInput();
        } 
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            OnInstructionOpened?.Invoke();

            BlockPlayerInput();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            OnInstructionClosed?.Invoke();

            EnablePlayerInput();
        }
    }

    private void BlockPlayerInput()
        {
        if (_orchestrator)
        {
            _orchestrator.IsBlocked = true;
        }
    }

    private void EnablePlayerInput()
    {
        if (_orchestrator)
        {
            _orchestrator.IsBlocked = false;
        }
    }

    private void BlockAllInput()
    {
        _isBlocked = true;
        BlockPlayerInput();
    }

    private void EnableAllInput()
    {
        _isBlocked = false;
        EnablePlayerInput();
    }
}
