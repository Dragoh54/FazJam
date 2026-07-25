using Items;
using Rooms;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [Header("Door Connection")]
    [SerializeField] public Door connectedDoor;
    
    [Header("Spawn")]
    [SerializeField] protected Transform spawnPoint;
    
    [Header("Room")]
    [SerializeField] protected Room room;

    private StepManager _stepManager;
    private GameManager _gameManager;

    public delegate void RoomVisited(Room room, Door door);
    public event RoomVisited OnRoomVisited;

    protected virtual void Awake()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
        _stepManager = FindAnyObjectByType<StepManager>();
    }

    public virtual void Interact()
    {
        var player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && connectedDoor != null && _stepManager.CurrentSteps > 0)
        {
            player.transform.position = connectedDoor.spawnPoint.position;

            connectedDoor.OnRoomVisited?.Invoke(connectedDoor.room, connectedDoor);

            _gameManager.CurrentRoom = connectedDoor.room;

            var roomCenter = connectedDoor.room.GetCenter();

            Camera.main.transform.position = new Vector3(
                roomCenter.x,
                roomCenter.y,
                Camera.main.transform.position.z
            );
        }
    }
}
