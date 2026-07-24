using TMPro;
using UnityEngine;

public class StepManager : MonoBehaviour
{
    [field: SerializeField]
    public int CurrentSteps { get; private set; } = 30;

    [field: SerializeField]
    public int MaxSteps { get; private set; } = 30;

    [SerializeField]
    private TextMeshProUGUI _uiCounter;

    [SerializeField]
    private TextMeshProUGUI _uiMaxCounter;

    private void Awake()
    {
        var doors = FindObjectsByType<Door>(FindObjectsInactive.Exclude);

        foreach (var door in doors)
        {
            door.OnRoomVisited += HandleRoomVisited;
        }

        UpdateUICounter();
        UpdateUIMaxSteps();
    }

    private void HandleRoomVisited(Room room, Door door)
    {
        SubtractStep();
    }

    private void SubtractStep()
    {
        if (CurrentSteps == 0)
        {
            return;
        }

        CurrentSteps--;

        UpdateUICounter();
    }

    private void AddSteps(int steps)
    {
        CurrentSteps = Mathf.Clamp(CurrentSteps + steps, 0, MaxSteps);
        UpdateUICounter();
    }

    private void IncreaseMaxSteps(int steps)
    {
        MaxSteps += steps;
        UpdateUIMaxSteps();
    }

    private void UpdateUICounter()
    {
        if (_uiCounter is not null)
        {
            _uiCounter.text = CurrentSteps.ToString();
        }
    }

    private void UpdateUIMaxSteps()
    {
        if (_uiMaxCounter is not null)
        {
            _uiMaxCounter.text = MaxSteps.ToString();
        }
    }
}
