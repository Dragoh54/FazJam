using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void MapInteraction(Vector3? currentPosition);
    public event MapInteraction OnMapOpened;
    public event MapInteraction OnMapClosed;

    [SerializeField]
    private PlayerControlsOrchestrator _orchestrator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OnMapOpened?.Invoke(Camera.main.transform.position);

            if (_orchestrator)
            {
                _orchestrator.IsBlocked = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            OnMapClosed?.Invoke(null);

            if (_orchestrator)
            {
                _orchestrator.IsBlocked = false;
            }
        }
    }
}
