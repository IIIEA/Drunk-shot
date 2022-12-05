using UnityEngine;

public class LevelBordersMover : MonoBehaviour
{
    [SerializeField] private Ball _ball;

    private float _offset;

    private void Start()
    {
        _offset = transform.position.y - _ball.transform.position.y;    
    }

    private void OnEnable()
    {
        _ball.Waited += OnWaited;
    }

    private void OnDisable()
    {
        _ball.Waited += OnWaited;
    }

    private void OnWaited(GameObject basket)
    {
        transform.position = new Vector3(transform.position.x, _ball.transform.position.y + _offset, 0);
    }
}
