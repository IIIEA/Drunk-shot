using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Basket : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _ball;

    [SerializeField] private GameObject _net;
    [SerializeField] private Transform _restingPoint;
    [SerializeField] private BasketData _data;

    private float _chargePower = 0;
    private bool _isEnoughtPower = false;
    private Vector3 _defaultNetScale;
    private Tween _scaleTween;
    private bool _catched = false;

    public BasketData Data => _data;
    public Transform RestingPoint => _restingPoint;
    public float ChargePower => _chargePower;

    public event Action Charged;
    public event Action Realized;

    private void Start()
    {
        _defaultNetScale = _net.transform.localScale;
    }

    public void SetCharge(float charge)
    {
        charge = Mathf.Clamp(charge * _data.AdditionalChargePower, 0, _data.MaxChargePower);

        _net.transform.localScale = new Vector3(_defaultNetScale.x, _defaultNetScale.y * Remap.DoRemap(0, _data.MaxChargePower, _defaultNetScale.y, _data.MaxNetScale, charge), _defaultNetScale.z);

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

        _scaleTween = _net.transform.DOScaleY(_defaultNetScale.y * _data.MaxNegativeScale, _data.NetRealizeTime);

        yield return _scaleTween.WaitForCompletion();

        ShootBall(_chargePower);
        Realized?.Invoke();

        _chargePower = 0;

        _scaleTween = _net.transform.DOScaleY(_defaultNetScale.y, _data.ReturnTime);
        _scaleTween.OnComplete(() => _net.transform.localScale = _defaultNetScale);
    }

    private void ShootBall(float power)
    {
        var ball = Instantiate(_ball, _restingPoint.transform.position, Quaternion.identity);

        ball.AddForce(transform.up * power, ForceMode2D.Impulse);
    }
}
