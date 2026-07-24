using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Rooms
{
    public class Room : MonoBehaviour
    {
        [field: SerializeField] public Door UpDoor;
        [field: SerializeField] public Door DownDoor;
        [field: SerializeField] public Door LeftDoor;
        [field: SerializeField] public Door RightDoor;

        [field: SerializeField] public int doorSetup;

        private static readonly (bool Up, bool Down, bool Left, bool Right)[] DoorLookup =
        {
            (true,  true,  true,  true ),
            (true,  true,  true,  false),
            (true,  true,  false, true ),
            (true,  false, true,  true ),
            (false, true,  true,  true ),
            (true,  true,  false, false),
            (false, false, true,  true ),
            (true,  false, false, true ),
            (false, true,  false, true ),
            (false, true,  true,  false),
            (true,  false, true,  false),
            (false, true,  false, false),
            (false, false, true,  false),
            (true,  false, false, false),
            (false, false, false, true ),
        };

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

        public void ApplyDoorSetup()
        {
            var setup = DoorLookup[doorSetup];

            UpDoor.gameObject.SetActive(setup.Up);
            DownDoor.gameObject.SetActive(setup.Down);
            LeftDoor.gameObject.SetActive(setup.Left);
            RightDoor.gameObject.SetActive(setup.Right);
        }

        private void SetDoor(Door ownedDoor, Door connection)
        {
            if(ownedDoor is not null)
            {
                ownedDoor.connectedDoor = connection;
            }
        }
    }
}
