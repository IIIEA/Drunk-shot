using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Basket", menuName = "Data/Basket Data", order = 51)]
public class BasketData : ScriptableObject
{
    [SerializeField, Min(1)] private float _additionalChargePower;
    [SerializeField] private float _minChargePower;
    [SerializeField] private float _maxChargePower;
    [Header("Net settings")]
    [SerializeField, Range(1,5)] private float _additionalScaleCoeff;
    [SerializeField] private float _maxNetScale;
    [SerializeField] private float _maxNegativeScale;
    [SerializeField] private float _minNetScale;
    [Header("Animation net timing")]
    [SerializeField] private float _netBounceTime;
    [SerializeField] private float _netRealizeTime;
    [SerializeField] private float _returnTime;

    public float AdditionlScaleCoeff => _additionalScaleCoeff;
    public float MinChargePower => _minChargePower;
    public float MaxChargePower => _maxChargePower;
    public float MaxNetScale => _maxNetScale;
    public float MaxNegativeScale => _maxNegativeScale;
    public float MinNetScale => _minNetScale;
    public float NetBounceTime => _netBounceTime;
    public float NetRealizeTime => _netRealizeTime;
    public float ReturnTime => _returnTime;
    public float AdditionalChargePower => _additionalChargePower;
}