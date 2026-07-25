using Doors;
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

    [SerializeField]
    private FinalExitDoor _finalExitDoor;

    [SerializeField]
    private IntroCutscene _cutscene;

    private void Awake()
    {
        _stepManager.OnStepsEnded += HandleStepsEnded;
        _finalExitDoor.OnEscaped += HandleEscape;
        _cutscene.OnCutsceneEnded += HandleCutsceneEnded;
    }

    private void HandleCutsceneEnded()
    {
        _uiManager.ShowInstructionPanel();
    }

    private void HandleStepsEnded(Room room)
    {
        var items = room.GetComponentsInChildren<Item>();
        var containsConsumable = items.Any(item => item.ItemData.CategoryType == ItemCategoryType.Consumable);

        var hasConsumableInInventory = _inventoryManager.ConsumeItem != null;

        if(hasConsumableInInventory || containsConsumable)
        {
            return;
        }

        _uiManager.ShowFailScreen();
        StartCoroutine(FailGame());
    }

    private void HandleEscape()
    {
        _uiManager.ShowWinScreen();
        StartCoroutine(RestartScene());
    }

    private IEnumerator FailGame()
    {
        yield return new WaitForSeconds(2);

        yield return RestartScene();
    }

    private IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(5);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
