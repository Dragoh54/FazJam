using System.Collections;
using UnityEngine;

public class ZoomOutHandler : MonoBehaviour
{
    [SerializeField]
    private Mansion _mansion;

    [SerializeField]
    private int _zoomFactor;

    [SerializeField]
    private int _baseZoom;

    [SerializeField]
    private float _zoomDuration;

    [SerializeField]
    private InputManager _inputManager;

    private Camera _camera;
    private GameManager _gameManager;
    private Coroutine _zoomCoroutine;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _gameManager = FindAnyObjectByType<GameManager>();

        _inputManager.OnMapOpened += ZoomOut;
        _inputManager.OnMapClosed += RevertZoom;
    }

    public void ZoomOut()
    {
        StartCameraTransition(
            _mansion.GetCenter(),
            _zoomFactor,
            _zoomDuration);
    }

    public void RevertZoom()
    {
        var roomPosition = _gameManager.CurrentRoom ? 
            _gameManager.CurrentRoom.GetCenter() : 
            GameObject.FindGameObjectWithTag("Player").transform.position;

        StartCameraTransition(
            roomPosition,
            _baseZoom,
            _zoomDuration);
    }

    private void StartCameraTransition(Vector3 targetPosition, float targetZoom, float duration)
    {
        if (_zoomCoroutine != null)
            StopCoroutine(_zoomCoroutine);

        _zoomCoroutine = StartCoroutine(CameraTransition(targetPosition, targetZoom, duration));
    }

    private IEnumerator CameraTransition(Vector3 targetPosition, float targetZoom, float duration)
    {
        Vector3 startPosition = _camera.transform.position;
        float startZoom = _camera.orthographicSize;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            t = Mathf.SmoothStep(0f, 1f, t);

            _camera.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            _camera.orthographicSize = Mathf.Lerp(startZoom, targetZoom, t);

            yield return null;
        }

        _camera.transform.position = targetPosition;
        _camera.orthographicSize = targetZoom;

        _zoomCoroutine = null;
    }
}
