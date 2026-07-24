using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private StepManager _stepManager;

    [SerializeField]
    private UIManager _uiManager;

    private void Awake()
    {
        _stepManager.OnStepsEnded += HandleStepsEnded;
    }

    private void HandleStepsEnded()
    {
        //REMOVE LATER
        return;

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
