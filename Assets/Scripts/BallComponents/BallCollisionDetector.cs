using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class BallCollisionDetector : MonoBehaviour
{
    public UnityEvent Collided;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collided?.Invoke();
    }
}
