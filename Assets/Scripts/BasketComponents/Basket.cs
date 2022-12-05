using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

public class Basket : MonoBehaviour
{
    [SerializeField] private GameObject _net;
    [SerializeField] private Transform _restingPoint;
    [SerializeField] private BasketData _data;
    [SerializeField] private BallCatcher _ballCatcher;
    [SerializeField] private RingEffects _effects;

    private float _chargePower = 0;
    private bool _isComplited = false;
    private bool _isEnoughtPower = false;
    private Vector3 _defaultNetScale;
    private Vector2 _defaultRestingPointPosition;
    private float _minYRestingPointPosition;
    private Tween _scaleTween;
    private bool _realised = false;
    private Ball _catchedBall;
    private WaitForSeconds _delay;

    public BasketData Data => _data;
    public Transform RestingPoint => _restingPoint;
    public float ChargePower => _chargePower;
    public bool IsComplited => _isComplited;

    public event Action Charged;
    public event Action Realized;
    public event Action<Basket> Complited;
    public event Action Restored;

    private void Start()
    {
        _delay = new WaitForSeconds(_data.BasketCooldown);
        _defaultRestingPointPosition = _restingPoint.localPosition;
        _minYRestingPointPosition = _restingPoint.localPosition.y - _data.CatchPointOffset;
        _defaultNetScale = _net.transform.localScale;
    }

    private void OnEnable()
    {
        _ballCatcher.BallCatched += OnBallCatched;
    }

    private void OnDisable()
    {
        _ballCatcher.BallCatched -= OnBallCatched;
    }

    private void OnBallCatched(Ball ball)
    {
        if(_isComplited == false)
        {
            _isComplited = true;
            Complited?.Invoke(this);
        }

        _catchedBall = ball;

        transform.DORotate(Vector2.up, 0.2f);

        ball.transform.DOMove(Utils.GetPositionWithYOffset(_restingPoint.position, 0.2f), 0.15f)
            .OnComplete(() => ball.transform.DOMove(_restingPoint.position, 0.15f));

        PlayNetBounceAnimation();
    }

    public void SetCharge(float charge)
    {
        charge = Mathf.Clamp(charge * _data.AdditionalChargePower, 0, _data.MaxChargePower);

        _net.transform.localScale = new Vector3(_defaultNetScale.x, _defaultNetScale.y * Remap.DoRemap(0, _data.MaxChargePower, _defaultNetScale.y, _data.MaxNetScale, charge), _defaultNetScale.z);
        _restingPoint.localPosition = new Vector2(0, Remap.DoRemap(_defaultNetScale.y, _data.MaxNetScale, _defaultRestingPointPosition.y, _minYRestingPointPosition, _net.transform.localScale.y));

        _catchedBall.transform.position = _restingPoint.transform.position;

        _chargePower = charge;

        if(_isEnoughtPower == false && _chargePower >= _data.MinChargePower)
        {
            _isEnoughtPower = true;
            Charged?.Invoke();
        }
        else if (_isEnoughtPower == true && _chargePower < _data.MinChargePower)
        {
            _isEnoughtPower = false;
            Realized?.Invoke();
        }
    }

    public void SetRotation(Vector2 direction)
    {
        transform.up = direction.normalized;
    }

    public void Realize()
    {
        StartCoroutine(RealizeRoutine());
    }

    private IEnumerator RealizeRoutine()
    {
        if (_scaleTween != null)
            _scaleTween.Complete();

        Realized?.Invoke();

        _restingPoint.localPosition = _defaultRestingPointPosition;

        if (_chargePower >= _data.MinChargePower)
        {
            _catchedBall.Shoot(_chargePower * transform.up);
            _realised = true;
        }
        else
        {
            _catchedBall.transform.DOMove(_restingPoint.position, 0.1f);
        }

        _scaleTween = _net.transform.DOScaleY(_defaultNetScale.y * _data.MaxNegativeScale, _data.NetRealizeTime);

        yield return _scaleTween.WaitForCompletion();

        _chargePower = 0;

        _scaleTween = _net.transform.DOScaleY(_defaultNetScale.y, _data.ReturnTime);
        _scaleTween.OnComplete(() => _net.transform.localScale = _defaultNetScale);

        yield return _delay;

        if (_realised)
        {
            _realised = false;
            _ballCatcher.gameObject.SetActive(true);
        }
    }

    private void PlayNetBounceAnimation()
    {
        _net.transform.DOScaleY(_data.MinNetScale * _defaultNetScale.y, _data.NetRealizeTime)
            .OnComplete(() => _net.transform.DOScaleY(_defaultNetScale.y, _data.NetRealizeTime));
    }

    public void Dispose()
    {
        gameObject.transform.DOScale(0, 0.3f).OnComplete(() => gameObject.SetActive(false));
    }

    public void Restore()
    {
        Restored?.Invoke();

        _isComplited = false;
    }
}
