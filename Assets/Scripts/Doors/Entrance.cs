using Items;
using Rooms;
using UnityEngine;

public class Entrance : MonoBehaviour, IInteractable
{
    [Header("Door Connection")]
    [SerializeField] public Entrance connectedEntrance;

    [Header("Spawn")]
    [SerializeField] protected Transform spawnPoint;

    [Header("Room")]
    [SerializeField] protected Room room;

    public virtual void Interact()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && connectedEntrance != null)
        {
            player.transform.position = connectedEntrance.spawnPoint.position;

            var playerCameraScript = player.GetComponent<PlayerCamera>();

            if (connectedEntrance.room)
            {
                playerCameraScript.enabled = false;

                var roomCenter = connectedEntrance.room.GetCenter();

                Camera.main.transform.position = new Vector3(
                    roomCenter.x,
                    roomCenter.y,
                    Camera.main.transform.position.z
                );
            }
            else
            {
                playerCameraScript.enabled = true;
            }
        }
    }
}
