using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private bool _isBlocked;

    private void Awake()
    {
        FindAnyObjectByType<InputManager>().OnMapOpened += HandleUIOpen;
        FindAnyObjectByType<InputManager>().OnInstructionOpened += HandleUIOpen;
        FindAnyObjectByType<InputManager>().OnMapClosed += HandleUIClose;
        FindAnyObjectByType<InputManager>().OnInstructionClosed += HandleUIClose;
    }

    void Update()
    {
        if (_isBlocked)
        {
            return;
        }

        Camera.main.transform.position = gameObject.transform.position;
    }

    private void HandleUIOpen()
    {
        _isBlocked = true;
    }

    private void HandleUIClose()
    {
        _isBlocked = false;
    }
}
