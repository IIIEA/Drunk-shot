using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Basket", menuName = "Data/Basket Data", order = 51)]
public class BasketData : ScriptableObject
{
    [SerializeField] private float _basketCooldown;
    [SerializeField, Min(1)] private float _additionalChargePower;
    [SerializeField] private float _minChargePower;
    [SerializeField] private float _maxChargePower;
    [Header("Net settings")]
    [SerializeField] private float _catcPointOffset;
    [SerializeField] private float _maxNetScale;
    [SerializeField] private float _maxNegativeScale;
    [SerializeField] private float _minNetScale;
    [Header("Animation net timing")]
    [SerializeField] private float _netBounceTime;
    [SerializeField] private float _netRealizeTime;
    [SerializeField] private float _returnTime;

    public float CatchPointOffset => _catcPointOffset;
    public float MinChargePower => _minChargePower;
    public float MaxChargePower => _maxChargePower;
    public float MaxNetScale => _maxNetScale;
    public float MaxNegativeScale => _maxNegativeScale;
    public float MinNetScale => _minNetScale;
    public float NetBounceTime => _netBounceTime;
    public float NetRealizeTime => _netRealizeTime;
    public float ReturnTime => _returnTime;
    public float AdditionalChargePower => _additionalChargePower;
    public float BasketCooldown => _basketCooldown;
}