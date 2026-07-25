using System.Collections;
using Text;
using UnityEngine;

public class IntroCutscene : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TypewriterText typewriter;

    [Header("UI")]
    [SerializeField] private CanvasGroup continuePrompt;

    [Header("Settings")]
    [SerializeField] private float continueDelay = 3f;
    [SerializeField] private float continueFadeDuration = 0.5f;

    [Header("Player")]
    [SerializeField] private GameObject player;

    private bool _canContinue;
    private bool _finished;

    public delegate void CutsceneEnded();
    public event CutsceneEnded OnCutsceneEnded;

    private void Awake()
    {
        player.SetActive(false);

        continuePrompt.alpha = 0f;
        continuePrompt.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        typewriter.OnTypingFinished += HandleTypingFinished;
    }

    private void OnDisable()
    {
        typewriter.OnTypingFinished -= HandleTypingFinished;
    }

    private void Start()
    {
        typewriter.Play();
    }

    private void Update()
    {
        if (_finished)
            return;

        if (!IsContinuePressed())
            return;

        if (typewriter.IsTyping)
        {
            typewriter.Skip();
            return;
        }

        if (_canContinue)
        {
            Continue();
        }
    }

    private bool IsContinuePressed()
    {
        return Input.GetMouseButtonDown(0)
               || Input.GetKeyDown(KeyCode.Space)
               || Input.GetKeyDown(KeyCode.Return);
    }

    private void HandleTypingFinished()
    {
        StartCoroutine(ShowContinueRoutine());
    }

    private IEnumerator ShowContinueRoutine()
    {
        continuePrompt.gameObject.SetActive(true);
        continuePrompt.alpha = 0f;

        var waitTime = Mathf.Max(0f, continueDelay - continueFadeDuration);

        yield return new WaitForSeconds(waitTime);

        var timer = 0f;

        while (timer < continueFadeDuration)
        {
            timer += Time.deltaTime;

            continuePrompt.alpha = Mathf.Lerp(
                0f,
                1f,
                timer / continueFadeDuration);

            yield return null;
        }

        continuePrompt.alpha = 1f;
        _canContinue = true;
    }

    private void Continue()
    {
        _finished = true;

        player.SetActive(true);

        gameObject.SetActive(false);

        OnCutsceneEnded?.Invoke();
    }
}