using Doors;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class InteractionPrompt : MonoBehaviour
{
    [SerializeField] private GameObject promptObject;
    
    private void Awake()
    {
        //var storyDoors = FindObjectsByType<StoryDoor>();

        //foreach (var door in storyDoors)
        //{
        //    door.OnFailToInteract += Handle;
        //}

        Hide();
    }
    
    public void Show()
    {
        promptObject.SetActive(true);
    }


    public void Hide()
    {
        promptObject.SetActive(false);
    }
}
