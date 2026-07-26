using Managers;
using Rooms;
using UnityEngine;

public class StepManager : MonoBehaviour
{
    [field: SerializeField]
    public int CurrentSteps { get; private set; } = 30;

    [field: SerializeField]
    public int MaxSteps { get; private set; } = 30;


    [field: SerializeField]
    private UIManager _uiManager;

    public delegate void StepsEnded(Room room);
    public event StepsEnded OnStepsEnded;

    private Room _currentRoom;

    private void Awake()
    {
        var doors = FindObjectsByType<Door>(FindObjectsInactive.Exclude);

        foreach (var door in doors)
        {
            door.OnRoomVisited += HandleRoomVisited;
        }

        _uiManager.UpdateUICounter(CurrentSteps);
        _uiManager.UpdateUIMaxSteps(MaxSteps);
    }

    private void HandleRoomVisited(Room room, Door door)
    {
        _currentRoom = room;
        SubtractStep();
    }

    private void SubtractStep()
    {
        if (CurrentSteps == 0)
        {
            return;
        }

        CurrentSteps--;

        if(CurrentSteps == 0)
        {
            OnStepsEnded?.Invoke(_currentRoom);
        }

        _uiManager.UpdateUICounter(CurrentSteps);
    }

    public void AddSteps(int steps)
    {
        int previousSteps = CurrentSteps;
        CurrentSteps = Mathf.Clamp(CurrentSteps + steps, 0, MaxSteps);

        _uiManager.AnimateStepsChange(previousSteps, CurrentSteps);
    }

    public void RestoreToMaxSteps()
    {
        CurrentSteps = MaxSteps;
        _uiManager.UpdateUICounter(CurrentSteps);
    }

    public void IncreaseMaxSteps(int steps)
    {
        MaxSteps += steps;
        _uiManager.UpdateUIMaxSteps(MaxSteps);
    }
}
