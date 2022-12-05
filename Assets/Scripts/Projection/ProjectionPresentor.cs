using System.Collections;
using UnityEngine;

public class ProjectionPresentor : MonoBehaviour, IMovableObjectHandler
{
    [SerializeField] private Rigidbody2D _simulatedRigidbody;

    private Basket _basket;
    private Projection _projection;
    private Coroutine _simulateRoutine;

    private void Start()
    {
        _projection = GetComponentInChildren<Projection>();

        _projection.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if(_basket != null)
        {
            _basket.Charged -= OnBasketCharged;
            _basket.Realized -= OnBasketRealized;
        }
    }

    private void OnBasketCharged()
    {
        _simulateRoutine = StartCoroutine(SimulateProjection());
    }

    private void OnBasketRealized()
    {
        if(_simulateRoutine != null)
        {
            StopCoroutine(_simulateRoutine);
            _simulateRoutine = null;
            _projection.gameObject.SetActive(false);
        }
    }

    private IEnumerator SimulateProjection()
    {
        _projection.gameObject.SetActive(true);

        while (_basket != null)
        {
            _projection.Simulate(_simulatedRigidbody, _basket.RestingPoint.position, _basket.RestingPoint.up * _basket.ChargePower);

            var alpha = Mathf.Clamp(Remap.DoRemap(_basket.Data.MinChargePower, _basket.Data.MaxChargePower - _basket.Data.MinChargePower * 30 / 100, 0.3f, 1, _basket.ChargePower), 0, 1);
            _projection.SetDotsAlpha(alpha);

            yield return null;
        }
    }

    public void Inject(GameObject dependency)
    {
        if (dependency == null)
            return;

        if (dependency.TryGetComponent(out Basket basket))
        {
            if (_basket != null)
            {
                _basket.Charged -= OnBasketCharged;
                _basket.Realized -= OnBasketRealized;
            }

            _basket = basket;

            _basket.Charged += OnBasketCharged;
            _basket.Realized += OnBasketRealized;
        }
    }
}