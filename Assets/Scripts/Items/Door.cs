using Items;
using UnityEngine;
using UnityEngine.Serialization;

public class Door : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Pretty fazballs");
    }
}
