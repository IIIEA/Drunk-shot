using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Basket : MonoBehaviour
{
    [SerializeField] private GameObject _net;
    [SerializeField] private BasketData _data;

    private float _chargePower = 0;
    private Vector3 _defaultNetScale;
    private Tween _scaleTween;
    private bool _catched = false;

    private void Start()
    {
        _defaultNetScale = _net.transform.localScale;
    }

    public void SetCharge(float charge)
    {
        charge = Mathf.Clamp(charge * _data.AdditionlScaleCoeff, 0, _data.MaxChargePower);

        _net.transform.localScale = new Vector3(_defaultNetScale.x, _defaultNetScale.y * Remap.DoRemap(0, _data.MaxChargePower, _defaultNetScale.y, _data.MaxNetScale, charge), _defaultNetScale.z);

        _chargePower = charge;
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

        //ShootBall();

        _scaleTween = _net.transform.DOScaleY(_defaultNetScale.y, _data.ReturnTime);
        _scaleTween.OnComplete(() => _net.transform.localScale = _defaultNetScale);
    }

    private void ShootBall(float power)
    {

    }
}
