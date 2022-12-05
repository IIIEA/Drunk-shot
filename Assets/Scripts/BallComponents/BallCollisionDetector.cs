using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class BallCollisionDetector : MonoBehaviour
{
    public UnityEvent CollidedWithBasket;
    public UnityEvent CollidedWithOthers;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            CollidedWithBasket?.Invoke();
        }
        else
        {
            CollidedWithOthers?.Invoke();
        }
    }
}
