using UnityEngine;

public class FogOfWarHandler : MonoBehaviour
{
    [SerializeField] private GameObject _fogOfWar;

    private Door[] _doors = new Door[4];

    void Awake()
    {
        _fogOfWar.SetActive(true);

        _doors = GetComponentsInChildren<Door>();

        foreach (Door door in _doors)
        {
            door.OnRoomVisited += HandleRoomVisited;
        }
    }

    void OnDisable()
    {
        foreach (Door door in _doors)
        {
            door.OnRoomVisited -= HandleRoomVisited;
        }
    }

    private void HandleRoomVisited(Room room, Door door)
    {
        _fogOfWar.SetActive(false);
    }
}
