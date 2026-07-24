using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _instructionPanel;

    [SerializeField]
    private GameObject _failPanel;

    [SerializeField]
    private GameObject _successPanel;

    [SerializeField]
    private TextMeshProUGUI _counter;

    [SerializeField]
    private TextMeshProUGUI _maxCounter;

    [SerializeField]
    private TextMeshProUGUI _addSteps;

    private Coroutine _stepsAnimation;

    private void Awake()
    {
        _instructionPanel.SetActive(false);

        var inputManager = FindAnyObjectByType<InputManager>();

        inputManager.OnInstructionOpened += HandleInstructionOpened;
        inputManager.OnInstructionClosed += HandleInstructionClosed;
    }

    public void ShowFailScreen()
    {
        _failPanel.SetActive(true);
    }

    public void ShowWinScreen()
    {
        _successPanel.SetActive(true);
    }

    public void UpdateUICounter(int steps)
    {
        if (_counter is not null)
        {
            _counter.text = steps.ToString();
        }
    }

    public void UpdateUIMaxSteps(int steps)
    {
        if (_maxCounter is not null)
        {
            _maxCounter.text = steps.ToString();
        }
    }

    private void HandleInstructionOpened()
    {
        _instructionPanel.SetActive(true);
    }

    private void HandleInstructionClosed()
    {
        _instructionPanel.SetActive(false);
    }

    public void AnimateStepsChange(int start, int finish)
    {
        if (_stepsAnimation != null)
            StopCoroutine(_stepsAnimation);

        _stepsAnimation = StartCoroutine(AnimateSteps(start, finish));
    }

    private IEnumerator AnimateSteps(int from, int to)
    {
        const float popupDelay = 1f;
        const float popupDuration = 0.35f;
        const float counterDuration = 0.5f;
        const float popupDistance = 40f;

        int difference = to - from;

        RectTransform rect = _addSteps.rectTransform;
        Vector2 startPos = rect.anchoredPosition;

        Color color = _addSteps.color;

        rect.anchoredPosition = startPos;
        color.a = 1f;
        _addSteps.color = color;
        _addSteps.text = $"+{difference}";
        _addSteps.gameObject.SetActive(true);

        yield return new WaitForSeconds(popupDelay);

        float elapsed = 0f;

        while (elapsed < popupDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / popupDuration);

            rect.anchoredPosition = startPos + Vector2.up * popupDistance * t;

            color.a = 1f - t;
            _addSteps.color = color;

            yield return null;
        }

        rect.anchoredPosition = startPos;
        color.a = 1f;
        _addSteps.color = color;
        _addSteps.gameObject.SetActive(false);

        elapsed = 0f;

        while (elapsed < counterDuration)
        {
            elapsed += Time.deltaTime;

            int displayedSteps = Mathf.RoundToInt(
                Mathf.Lerp(from, to, elapsed / counterDuration));

            UpdateUICounter(displayedSteps);

            yield return null;
        }

        UpdateUICounter(to);

        _stepsAnimation = null;
    }
}
