using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _instructionPanel;

    [SerializeField]
    private GameObject _failPanel;

    [SerializeField]
    private GameObject _successPanel;

    private void Awake()
    {
        _instructionPanel.SetActive(false);

        var inputManager = FindAnyObjectByType<InputManager>();

        inputManager.OnInstructionOpened += HandleInstructionOpened;
        inputManager.OnInstructionClosed += HandleInstructionClosed;
    }

    public void ShowFailScreen()
    {
        _failPanel.SetActive(true);
    }

    public void ShowWinScreen()
    {
        _successPanel.SetActive(true);
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
