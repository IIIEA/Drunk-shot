using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class DeathZone : MonoBehaviour
{
    public UnityEvent GameEnded;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Ball ball))
        {
            GameEnded?.Invoke();
        }
    }
}
