using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    [SerializeField] private float _buffer;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;

        var (center, size) = CalculateOrthographicSize();

        _mainCamera.orthographicSize = size;
    }

    private (Vector3 center, float size) CalculateOrthographicSize()
    {
        var bounds = new Bounds();

        foreach (var collider in FindObjectsOfType<Collider2D>()) bounds.Encapsulate(collider.bounds);

        bounds.Expand(_buffer);

        var vertical = bounds.size.y;
        var horizontal = bounds.size.x * _mainCamera.pixelHeight / _mainCamera.pixelWidth;

        var size = Mathf.Max(horizontal, vertical) * 0.5f;
        var center = bounds.center + new Vector3(0, 0, -10);

        return (center, size);
    }
}
