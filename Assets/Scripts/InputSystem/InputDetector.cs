using System;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputDetector : MonoBehaviour
{
    private InputAsset _input;

    private Camera _mainCamera;

    public Vector3 TouchPosition => Utils.ScreenToWorld(_mainCamera, _input.Touch.PrimaryPosition.ReadValue<Vector2>());

    public event Action<Vector2> DragStarted;
    public event Action<Vector2> DragEnded;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _input = new InputAsset();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Start()
    {
        _input.Touch.PrimaryContact.performed += PerformTouch;
    }

    private void PerformTouch(InputAction.CallbackContext context)
    {
        switch (context.ReadValue<float>() == 0)
        {
            case true:
                DragEnded?.Invoke(Utils.ScreenToWorld(_mainCamera, _input.Touch.PrimaryPosition.ReadValue<Vector2>()));
                break;
            case false:
                DragStarted?.Invoke(Utils.ScreenToWorld(_mainCamera, _input.Touch.PrimaryPosition.ReadValue<Vector2>()));
                break;
        }
    }

    public float GetDragDistance(Vector2 startPosition)
    {
        return Vector2.Distance(startPosition, TouchPosition);
    }

    public Vector2 GetDragDirection(Vector3 startPosition)
    {
        var direction = (startPosition - TouchPosition).normalized;

        return direction;
    }
}
