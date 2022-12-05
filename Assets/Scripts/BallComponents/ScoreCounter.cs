using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private int _counter = 0;
    private int _points = 0;
    private int _multiple = 1;
    private int _bounceCounter = 2;
    private DunkType _dunkType;
    private bool _perfectLanded = true;

    public event Action<int, DunkType, int> ScoreChanged;

    private void Start()
    {
        _points = _counter;
        ScoreChanged?.Invoke(_counter, DunkType.Null, 0);
    }

    private void PerfectLanded()
    {
        switch (_dunkType)
        {
            case DunkType.Perfect:
                _dunkType = DunkType.Perfect;
                break;
            case DunkType.Bounce:
                _dunkType = DunkType.Both;
                break;
            case DunkType.Both:
                _dunkType = DunkType.Both;
                break;
            case DunkType.Null:
                _dunkType = DunkType.Perfect;
                break;
        }

        _multiple += 1;

        _counter += _multiple;
    }

    public void OnBounced()
    {
        switch (_dunkType)
        {
            case DunkType.Perfect:
                _dunkType = DunkType.Both;
                break;
            case DunkType.Bounce:
                _dunkType = DunkType.Bounce;
                break;
            case DunkType.Both:
                _dunkType = DunkType.Both;
                break;
            case DunkType.Null:
                _dunkType = DunkType.Bounce;
                break;
        }

        _counter += _bounceCounter;

        _counter += _multiple;
    }

    public void OnBasketCollided()
    {
        _dunkType = DunkType.Null;
        _perfectLanded = false;
        _multiple = 1;
    }

    public void OnLanded()
    {
        if (_perfectLanded)
        {
            PerfectLanded();
        }
        else
        {
            _multiple = 1;

            _perfectLanded = true;

            _counter += _multiple;
        }

        _points = _counter - _points;

        ScoreChanged?.Invoke(_counter, _dunkType, _points);

        _points = _counter;
    }
}

public enum DunkType
{
    Perfect,
    Bounce,
    Both,
    Null
}
