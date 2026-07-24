using UnityEngine;

public class Mansion : MonoBehaviour
{
    public int columns = 10;
    private Room[,] _gridArray;

    void Start()
    {
        PlaceChildrenInGrid();
        SetDoorsConnection();
    }

    public Vector3 GetCenter()
    {
        var centroid = new Vector3(0, 0, 0);

        if (transform.childCount > 0)
        {
            foreach (var child in _gridArray)
            {
                centroid += child.transform.position;
            }

            centroid /= (transform.childCount + 1);
        }

        return centroid;
    }

    void PlaceChildrenInGrid()
    {
        int totalChildren = transform.childCount;
        if (totalChildren == 0) return;

        int rows = Mathf.CeilToInt((float)totalChildren / columns);

        _gridArray = new Room[rows, columns];

        for (int i = 0; i < totalChildren; i++)
        {
            int row = i / columns;
            int column = i % columns;

            _gridArray[row, column] = transform.GetChild(i).GetComponent<Room>();
        }
    }

    void SetDoorsConnection()
    {
        var rows = _gridArray.GetLength(0);
        var columns = _gridArray.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                Door upDoorConnection = i > 0 ? _gridArray[i - 1, j]?.DownDoor : null; 
                Door downDoorConnection = i < rows - 1 ? _gridArray[i + 1, j]?.UpDoor : null; 
                Door leftDoorConnection = j > 0 ? _gridArray[i, j - 1]?.RightDoor : null; 
                Door rightDoorConnection = j < columns - 1 ? _gridArray[i, j + 1]?.LeftDoor : null;

                _gridArray[i, j]?.SetDoorConnection(upDoorConnection, downDoorConnection, leftDoorConnection, rightDoorConnection);
            }
        }
    }
}
