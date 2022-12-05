using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    public event Action<GameObject> Waited;
    public event Action Shooted;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.simulated = true;
    }

    public void Shoot(Vector2 force)
    {
        _rigidbody2D.simulated = true;
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.AddForce(force, ForceMode2D.Impulse);

        Shooted?.Invoke();
    }

    public void Catch(GameObject catcher)
    {
        _rigidbody2D.simulated = false;
        Waited?.Invoke(catcher);
    }
}
