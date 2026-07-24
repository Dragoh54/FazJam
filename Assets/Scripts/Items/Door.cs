using Items;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [Header("Door Connection")]
    [SerializeField] public Door connectedDoor;
    
    [Header("Spawn")]
    [SerializeField] private Transform spawnPoint;
    
    [Header("Room")]
    [SerializeField] private Room room;

    public delegate void RoomVisited(Room room, Door door);
    public event RoomVisited OnRoomVisited;

    public void Interact()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && connectedDoor != null)
        {
            player.transform.position = connectedDoor.spawnPoint.position;

            connectedDoor.OnRoomVisited?.Invoke(connectedDoor.room, connectedDoor);

            var roomCenter = connectedDoor.room.GetCenter();
            
            Camera.main.transform.position = new Vector3(
                roomCenter.x,
                roomCenter.y,
                Camera.main.transform.position.z
            );
        }
    }
}
