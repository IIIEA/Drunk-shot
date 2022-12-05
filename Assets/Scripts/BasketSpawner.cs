using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasketSpawner : MonoBehaviour
{
    [SerializeField] private CollectableItem _collectable;
    [SerializeField] private Basket _basketPrefab;
    [SerializeField] private Transform _leftBorder;
    [SerializeField] private Transform _rightBorder;
    [Header("Spawn settings")]
    [SerializeField] private float _maxYOffset;
    [SerializeField] private float _minYoffset;
    [SerializeField] private float _minXOffset;
    [SerializeField] private float _basketSizeX;
    [SerializeField] private float _maxRotateAngle;
    [SerializeField, Range(0, 100)] private int _collectableSpawnChance;
    [SerializeField] private float _collectableOffset;

    private List<Basket> _baskets;

    private void Awake()
    {
        var baskets = GetComponentsInChildren<Basket>().ToList();

        _baskets = new List<Basket>(baskets.Count);
        _baskets = baskets;
    }

    private void OnEnable()
    {
        if(_baskets.Count > 0)
        {
            foreach (var basket in _baskets)
            {
                basket.Complited += OnComplited;
            }
        }
    }

    private void OnDisable()
    {
        if (_baskets.Count > 0)
        {
            foreach (var basket in _baskets)
            {
                basket.Complited -= OnComplited;
            }
        }
    }

    private void OnComplited(Basket basket)
    {
        if (_baskets.Count > 0)
        {
            if (basket == _baskets[_baskets.Count - 1])
            {
                _baskets[0].Dispose();
                _baskets.RemoveAt(0);

                SpawnNewBasket();
            }
        }
    }

    private void SpawnNewBasket()
    {
        var basket = _baskets.Find(x => x.gameObject.activeSelf == false);

        if(basket == null)
        {
            basket = Instantiate(_basketPrefab);
        }

        SetupBasket(basket);
    }

    private void SetupBasket(Basket basket)
    {
        basket.transform.position = GetNewPosition(_baskets[_baskets.Count - 1].transform.position);
        basket.transform.eulerAngles = GetNewRotation(_baskets[_baskets.Count - 1].transform.position, basket.transform.position);
        basket.gameObject.SetActive(true);
        basket.transform.localScale = Vector3.zero;
        basket.transform.DOScale(_basketPrefab.transform.localScale, 0.3f);
        basket.transform.SetParent(transform);

        basket.Complited += OnComplited;

        SetCollectable(basket.transform);
        _baskets.Add(basket);
    }

    private void SetCollectable(Transform startedPosition)
    {
        if (Random.Range(0, 100) > _collectableSpawnChance)
            return;

        var spawnPosition = startedPosition.position + _collectableOffset * startedPosition.up;

        var collectable = Instantiate(_collectable, new Vector2(spawnPosition.x, spawnPosition.y), Quaternion.identity);

        collectable.transform.up = startedPosition.up;
    }

    private Vector2 GetNewPosition(Vector2 previousPosition)
    {
        var newPositionY = Random.Range(previousPosition.y + _minYoffset, previousPosition.y + _maxYOffset);
        var canSpawnRight = Mathf.Abs(previousPosition.x + _minXOffset) < _rightBorder.position.x;
        var canSpawnLeft = Mathf.Abs(previousPosition.x - _minXOffset) > _leftBorder.position.x;
        var newPositionX = 0f;

        if(canSpawnLeft && canSpawnRight)
        {
            newPositionX = Random.Range(0, 1) > 0.5 ?
                Random.Range(_leftBorder.position.x + _basketSizeX / 2, previousPosition.x - _minXOffset) : Random.Range(previousPosition.x + _maxYOffset, _rightBorder.position.x - _basketSizeX / 2);
        }
        else if (canSpawnLeft)
        {
            newPositionX = Random.Range(_leftBorder.position.x + _basketSizeX / 2, previousPosition.x - _minXOffset);
        }
        else if (canSpawnRight)
        {
            newPositionX = Random.Range(previousPosition.x + _minXOffset, _rightBorder.position.x - _basketSizeX / 2);
        }

        return new Vector2(newPositionX, newPositionY);
    }

    private Vector3 GetNewRotation(Vector2 previousPosition, Vector2 currentPosition)
    {
        var rotateY = currentPosition.x > previousPosition.x ? Random.Range(0, _maxRotateAngle) : Random.Range(0, -_maxRotateAngle);

        return new Vector3(0, 0, rotateY);
    }
}
