using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private ObjectDependencyInjector _injector;
    [SerializeField] private Transform _targetToFollow;
    [SerializeField] private float _cameraPositionOffset;
    [SerializeField] private float _downBorderOffset;
    [SerializeField] private float _followSpeed;
    [SerializeField] private float _deadZone;

    private Vector3 _lastDeadZonePosition;

    private void Update()
    {
        if (_injector.GameObject != null)
            _lastDeadZonePosition = _injector.GameObject.transform.position;


        if (Mathf.Abs(transform.position.y - _targetToFollow.position.y) >= _deadZone + _cameraPositionOffset)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, _targetToFollow.position.y + _cameraPositionOffset, transform.position.z);
            targetPosition.y = Mathf.Clamp(targetPosition.y, _lastDeadZonePosition.y - _downBorderOffset, targetPosition.y);

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, _followSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}
