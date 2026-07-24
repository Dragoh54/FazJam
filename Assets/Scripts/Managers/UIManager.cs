using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _instructionPanel;

    private void Awake()
    {
        _instructionPanel.SetActive(false);

        var inputManager = FindAnyObjectByType<InputManager>();

        inputManager.OnInstructionOpened += HandleInstructionOpened;
        inputManager.OnInstructionClosed += HandleInstructionClosed;
    }

    private void HandleInstructionOpened()
    {
        _instructionPanel.SetActive(true);
    }

    private void HandleInstructionClosed()
    {
        _instructionPanel.SetActive(false);
    }
}
