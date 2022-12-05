using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollectableItem : MonoBehaviour, ICollectable
{
    [SerializeField] private float _leaveTimer;
    [SerializeField] private float _bezierOffsetY;
    [SerializeField] private float _bezierOffsetX;
    [SerializeField] private float _offset;
    [SerializeField] private float _moveOffset;

    private Collider2D _collider2D;
    private float _timer = 0;
    private Vector3 _pointBezier1;
    private Vector3 _pointBezier2;
    private Vector3 _targetPoint;
    private Tween _idleTween;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        _collider2D.isTrigger = true;

        _targetPoint = new Vector3(3f, Utils.GetUpperScreenBorder(_offset), 0f);
        _pointBezier1 = new Vector3(transform.position.x, transform.position.y - _bezierOffsetY, 0);

        var middlePointY = Mathf.Abs(transform.position.y - _targetPoint.y) / 2;
        var middlePointX = Random.Range(0, 100) > 50 ? transform.position.x - _bezierOffsetX : transform.position.x + _bezierOffsetX;

        _pointBezier2 = new Vector3(middlePointX, middlePointY);
    }

    private void Start()
    {
        _idleTween = transform.DOLocalMove(transform.position + _moveOffset * transform.up, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
    }

    private void OnDisable()
    {
        if (_idleTween != null)
            _idleTween.Kill();
    }

    public void Collect()
    {
        _collider2D.enabled = false;

        _idleTween.Kill();
        _idleTween = transform.DORotate(new Vector3(0, 0, 180), 0.7f).SetLoops(-1, LoopType.Incremental);

        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (_timer <= _leaveTimer)
        {
            transform.position = MathBezier.GetPoint(transform.position, _pointBezier1, _pointBezier2, _targetPoint, _timer / _leaveTimer);
            _timer += Time.deltaTime;

            yield return null;
        }

        Destroy(gameObject);
    }
}
