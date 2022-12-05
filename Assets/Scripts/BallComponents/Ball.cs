using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private bool _simulatedState;

    public event Action<GameObject> Waited;
    public event Action Shooted;
    public UnityEvent Landed;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.simulated = true;

        _simulatedState = _rigidbody2D.simulated;
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

    public void FirstDunk()
    {
        Landed?.Invoke();
    }

    public void OnPaused(bool isPaused)
    {
        if (isPaused)
        {
            _simulatedState = _rigidbody2D.simulated;

            _rigidbody2D.simulated = !isPaused;
        }
        else
        {
            _rigidbody2D.simulated = _simulatedState;
        }
    }
}
