using System;
using Items;
using UnityEngine;
using UnityEngine.Serialization;

public class Door : MonoBehaviour, IInteractable
{
    [Header("Door Connection")]
    [SerializeField] private Door connectedDoor;
    
    [Header("Spawn")]
    [SerializeField] private Transform spawnPoint;
    
    [Header("Room")]
    [SerializeField] private Room room;

    public void Interact()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && connectedDoor != null)
        {
            player.transform.position = connectedDoor.spawnPoint.position;
            
            var roomCenter = connectedDoor.room.GetCenter();
            
            Camera.main.transform.position = new Vector3(
                roomCenter.x,
                roomCenter.y,
                Camera.main.transform.position.z
            );
        }
    }
}
