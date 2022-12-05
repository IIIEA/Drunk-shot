using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LootCollector : MonoBehaviour
{
    private int _counter;

    public int Counter => _counter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out ICollectable collectable))
        {
            collectable.Collect();
            _counter++;
        }
    }
}
