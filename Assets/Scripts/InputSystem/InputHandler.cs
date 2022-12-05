using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputDetector))]
public class InputHandler : MonoBehaviour, IMovableObjectHandler
{
    [SerializeField] private LayerMask _interactableMask;

    private InputDetector _inputDetector;
    private Coroutine _dragRoutine;
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
           _dragRoutine = StartCoroutine(DraggedRoutine(_basket, position));
        }
    }

    private void OnDragEnded(Vector2 position)
    {
        if (_dragRoutine != null)
        {
            StopCoroutine(_dragRoutine);
            _dragRoutine = null;

            _basket.Realize();
        }
    }

    private IEnumerator DraggedRoutine(Basket basket, Vector3 position)
    {
        while (basket != null)
        {
            basket.SetCharge(_inputDetector.GetDragDistance(position));
            basket.SetRotation(_inputDetector.GetDragDirection(position));
            yield return null;
        }
    }

    private bool CheckColliderHit(Vector2 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero, 1f, _interactableMask.value);

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
        if (dependency == null)
        {
            enabled = false;
            return;
        }
        else
        {
            enabled = true;
        }

        if(dependency.TryGetComponent(out Basket basket))
        {
            _basket = basket;
        }
    }
}
