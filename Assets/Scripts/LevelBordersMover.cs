using System.Collections;
using UnityEngine;

public class LevelBordersMover : MonoBehaviour
{
    [SerializeField] private Ball _ball;

    private Coroutine _moveRoutine;
    private float _offset;

    private void Start()
    {
        _offset = transform.position.y - _ball.transform.position.y;    
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

    private void OnShooted()
    {
        if (_moveRoutine != null)
            StopCoroutine(_moveRoutine);

        _moveRoutine = StartCoroutine(MoveRoutine());
    }

    private void OnWaited(GameObject basket)
    {
        if (_moveRoutine != null)
            StopCoroutine(_moveRoutine);
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            transform.position = new Vector3(transform.position.x, _ball.transform.position.y + _offset, 0);
            yield return null;
        }
    }
}
