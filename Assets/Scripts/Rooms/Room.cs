using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private Collider2D[] walls;

    public Vector3 GetCenter()
    {
        if (walls == null || walls.Length == 0)
            return transform.position;

        var bounds = walls[0].bounds;

        for (var i = 1; i < walls.Length; i++)
        {
            bounds.Encapsulate(walls[i].bounds);
        }

        return bounds.center;
    }
}
