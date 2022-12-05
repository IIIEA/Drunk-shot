using UnityEngine;

public class RingEffects : MonoBehaviour
{
    [SerializeField] private BallCatcher _ballCatcher;
    [SerializeField] private SpriteRenderer _upRing;
    [SerializeField] private SpriteRenderer _downRing;
    [SerializeField] private Color _color;

    private Color _defaultColor;

    private void Start()
    {
        _defaultColor = _upRing.color;    
    }

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

    public void Restore()
    {
        _upRing.color = _downRing.color = _defaultColor;
        enabled = true;
    }
}
