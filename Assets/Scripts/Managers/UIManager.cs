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
    private GameObject _shopPanel;

    [SerializeField]
    private TextMeshProUGUI _counter;

    [SerializeField]
    private TextMeshProUGUI _moneyCounter;

    [SerializeField]
    private TextMeshProUGUI _maxCounter;

    [SerializeField]
    private TextMeshProUGUI _addSteps;

    [SerializeField]
    private TextMeshProUGUI _addMoney;

    [SerializeField]
    private ShopPanel _shopPanelScript;

    private Coroutine _stepsAnimation;
    private Coroutine _moneyChangeAnimation;
    private Coroutine _moneyAnimation;

    public delegate void ShopClosed();
    public event ShopClosed OnShopClosed;

    private void Awake()
    {
        _instructionPanel.SetActive(false);

        var inputManager = FindAnyObjectByType<InputManager>();

        inputManager.OnInstructionOpened += HandleInstructionOpened;
        inputManager.OnInstructionClosed += HandleInstructionClosed;
    }

    public void HandleShopChange(bool hasConsumable, int currentMoney)
    {
        _shopPanel.SetActive(true);
        _shopPanelScript.HandleVisibility(hasConsumable, currentMoney);
    }

    public void HandleShopClosed()
    {
        OnShopClosed?.Invoke();

        _shopPanel.SetActive(false);
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

    public void AnimateMoneyChange(int start, int finish)
    {
        if (_moneyChangeAnimation != null)
            StopCoroutine(_moneyChangeAnimation);

        _moneyChangeAnimation = StartCoroutine(UpdateMoneyChangeUI(start, finish));
    }

    public void UpdateAddMoneyCounter(int money)
    {
        if (_addMoney is not null)
        {
            _addMoney.text = "+" + money.ToString();
        }
    }

    public void UpdateMoneyCounter(int money)
    {
        if (_moneyCounter is not null)
        {
            _moneyCounter.text = money.ToString();
        }
    }

    public IEnumerator UpdateMoneyChangeUI(int from, int to)
    {
        const float counterDuration = 0.5f;

        int difference = to - from;

        _addMoney.gameObject.SetActive(true);

        float elapsed = 0f;

        while (elapsed < counterDuration)
        {
            elapsed += Time.deltaTime;

            int displayedMoney = Mathf.RoundToInt(
                Mathf.Lerp(from, to, elapsed / counterDuration));

            UpdateAddMoneyCounter(displayedMoney);

            yield return null;
        }

        UpdateAddMoneyCounter(to);

        _moneyChangeAnimation = null;
    }

    public void AnimateMoneyCounter(int start, int finish)
    {
        if (_moneyAnimation != null)
            StopCoroutine(_moneyAnimation);

        _moneyAnimation = StartCoroutine(UpdateMoneyUI(start, finish));
    }

    public IEnumerator UpdateMoneyUI(int from, int to)
    {
        const float popupDuration = 0.35f;
        const float counterDuration = 0.5f;
        const float popupDistance = 40f;

        int difference = to - from;

        RectTransform rect = _addMoney.rectTransform;
        Vector2 startPos = rect.anchoredPosition;

        Color color = _addMoney.color;

        rect.anchoredPosition = startPos;
        color.a = 1f;
        _addMoney.color = color;
        _addMoney.text = $"+{difference}";

        float elapsed = 0f;

        while (elapsed < popupDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / popupDuration);

            rect.anchoredPosition = startPos + Vector2.down * popupDistance * t;

            color.a = 1f - t;
            _addMoney.color = color;

            yield return null;
        }

        rect.anchoredPosition = startPos;
        color.a = 1f;
        _addMoney.color = color;
        _addMoney.gameObject.SetActive(false);

        elapsed = 0f;

        while (elapsed < counterDuration)
        {
            elapsed += Time.deltaTime;

            int displayedMoney = Mathf.RoundToInt(
                Mathf.Lerp(from, to, elapsed / counterDuration));

            UpdateMoneyCounter(displayedMoney);

            yield return null;
        }

        UpdateMoneyCounter(to);

        _moneyAnimation = null;
    }
}
