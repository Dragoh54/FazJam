using TMPro;
using UnityEngine;

public class InstructionPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _clickTipText;

    public delegate void StartInstructionClosed();
    public event StartInstructionClosed OnStartInstructionClosed;

    [SerializeField]
    private float _inputDelay = 0.5f;
    private float _inputTimer = 0.0f;

    public void ShowWithClickTip()
    {
        this.gameObject.SetActive(true);

        _clickTipText.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (_inputTimer >= _inputDelay && Input.GetKey(KeyCode.Mouse0))
        {
            OnStartInstructionClosed?.Invoke();

            _clickTipText.gameObject.SetActive(false);

            this.gameObject.SetActive(false);

            this.enabled = false;
        }

        _inputTimer += Time.deltaTime;
    }
}
