using Items;
using Managers;
using Rooms;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private StepManager _stepManager;

    [SerializeField]
    private InventoryManager _inventoryManager;

    [SerializeField]
    private UIManager _uiManager;

    public Room CurrentRoom { get; set; }

    private void Awake()
    {
        _stepManager.OnStepsEnded += HandleStepsEnded;
    }

    private void HandleStepsEnded(Room room)
    {
        var items = room.GetComponentsInChildren<Item>();
        var containsConsumable = items.Any(item => item.ItemData.categoryType == ItemCategoryType.Consumable);

        var hasConsumableInInventory = _inventoryManager.ConsumeItem != null;

        if(hasConsumableInInventory || containsConsumable)
        {
            return;
        }

        _uiManager.ShowFailScreen();
        StartCoroutine(RestartScene());
    }

    private void HandleEscape()
    {
        _uiManager.ShowWinScreen();
        StartCoroutine(RestartScene());
    }

    private IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(5);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
