using System.Collections;
using UnityEngine;

public class BallRotator : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;

    private float _rotationZ;
    private Ball _ball;
    private Coroutine _rotateRoutine;

    private void Awake()
    {
        _ball = GetComponentInParent<Ball>();
    }

    private void OnEnable()
    {
        _ball.Waited += OnWaited;
        _ball.Shooted += OnShooted;
    }

    private void OnDisable()
    {
        _ball.Waited -= OnWaited;
        _ball.Shooted -= OnShooted;
    }

    private void OnWaited(GameObject basketObject)
    {
        if (_rotateRoutine != null)
            StopCoroutine(_rotateRoutine);
    }

    private void OnShooted()
    {
        _rotateRoutine = StartCoroutine(RotateRoutine());
    }

    private IEnumerator RotateRoutine()
    {
        while (true)
        {
            _rotationZ += _rotateSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, _rotationZ);

            yield return null;
        }
    }
}
