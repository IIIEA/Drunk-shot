using UnityEngine;

public class RingEffects : MonoBehaviour
{
    [SerializeField] private BallCatcher _ballCatcher;
    [SerializeField] private SpriteRenderer _upRing;
    [SerializeField] private SpriteRenderer _downRing;
    [SerializeField] private Color _color;

    private void OnEnable()
    {
        _ballCatcher.BallCatched += OnBallCatched;
    }

    private void OnDisable()
    {
        _ballCatcher.BallCatched -= OnBallCatched;
    }

    private void OnBallCatched(Ball ball)
    {
        _upRing.color = _downRing.color = _color;
        enabled = false;
    }
}
