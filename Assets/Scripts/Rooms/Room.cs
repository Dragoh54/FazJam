using UnityEngine;  

public class Room : MonoBehaviour
{
    [field: SerializeField] public Door UpDoor;
    [field: SerializeField] public Door DownDoor;
    [field: SerializeField] public Door LeftDoor;
    [field: SerializeField] public Door RightDoor;

    public Vector3 GetCenter()
    {
        return transform.position;
    }

    public void SetDoorConnection(Door upDoorConnection,
        Door downDoorConnection,
        Door leftDoorConnection,
        Door rightDoorConnection)
    {
        SetDoor(UpDoor, upDoorConnection);
        SetDoor(DownDoor, downDoorConnection);
        SetDoor(LeftDoor, leftDoorConnection);
        SetDoor(RightDoor, rightDoorConnection);
    }

    private void SetDoor(Door ownedDoor, Door connection)
    {
        if(ownedDoor is not null)
        {
            ownedDoor.connectedDoor = connection;
        }
    }
}
