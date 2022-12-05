using UnityEngine;

[RequireComponent(typeof(Ball))]
public class DependencySwitcher : MonoBehaviour
{
    [SerializeField] private ObjectDependencyInjector _injector;

    private Ball _ball;

    private void Awake()
    {
        _ball = GetComponent<Ball>();
    }

    private void OnEnable()
    {
        _ball.Waited += InjectObject;
        _ball.Shooted += OnShooted;
    }

    private void OnDisable()
    {
        _ball.Waited -= InjectObject;
        _ball.Shooted -= OnShooted;
    }

    private void OnShooted()
    {
        InjectObject(null);
    }

    private void InjectObject(GameObject injectedObject)
    {
        _injector.GameObject = injectedObject;
    }
}
