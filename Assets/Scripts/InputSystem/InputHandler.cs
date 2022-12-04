using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputDetector))]
public class InputHandler : MonoBehaviour, IMovableObjectHandler
{
    private InputDetector _inputDetector;
    private Basket _basket;
    private Camera _mainCamera;

    private void Awake()
    {
        _inputDetector = GetComponent<InputDetector>();
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _inputDetector.DragStarted += OnDragStarted;
        _inputDetector.DragEnded += OnDragEnded;
    }

    private void OnDisable()
    {
        _inputDetector.DragStarted -= OnDragStarted;
        _inputDetector.DragEnded -= OnDragEnded;
    }

    private void OnDragStarted(Vector2 position)
    {
        if (CheckColliderHit(position))
        {
            StartCoroutine(DraggedRoutine(_basket));
        }
    }

    private void OnDragEnded(Vector2 position)
    {
        StopCoroutine(DraggedRoutine(null));
    }

    private IEnumerator DraggedRoutine(Basket basket)
    {
        while (basket != null)
        {
            yield return null;
        }
    }

    private bool CheckColliderHit(Vector2 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);

        if (hit.collider == null)
        {
            return false;
        }

        if (hit.collider.TryGetComponent(out Basket basket))
        {
            if (_basket == basket)
            {
                return true;
            }
        }

        return false;
    }

    public void Inject(GameObject dependency)
    {
        if(dependency.TryGetComponent(out Basket basket))
        {
            _basket = basket;
        }
    }
}
