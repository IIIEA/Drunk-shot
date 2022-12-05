using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BallCatcher : MonoBehaviour
{
    private Basket _basket;
    private Collider2D _collider2D;

    public event Action<Ball> BallCatched;

    private void Awake()
    {
        _basket = GetComponentInParent<Basket>();

        _collider2D = GetComponent<CircleCollider2D>();
        _collider2D.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Ball ball))
        {
            if (_basket.IsComplited == false)
            {
                ball.FirstDunk();
            }

            BallCatched?.Invoke(ball);
            ball.Catch(_basket.gameObject);

            gameObject.SetActive(false);
        }
    }
}
