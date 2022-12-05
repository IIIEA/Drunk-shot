using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LootCollector : MonoBehaviour
{
    private int _counter = 0;

    public event Action<int> StarsCountChanged;

    private void Start()
    {
        StarsCountChanged?.Invoke(_counter);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out ICollectable collectable))
        {
            collectable.Collect();
            _counter++;

            StarsCountChanged?.Invoke(_counter);
        }
    }
}
