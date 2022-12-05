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

    private void FixedUpdate()
    {
        if (_injector.GameObject != null)
            _lastDeadZonePosition = _injector.GameObject.transform.position;

        if (Mathf.Abs(transform.position.y - _targetToFollow.position.y) >= _deadZone + _cameraPositionOffset)
        {
            Vector3 currentPosition = transform.position;
            Vector3 targetPosition = new Vector3(currentPosition.x,
                Mathf.Clamp(_targetToFollow.position.y + _cameraPositionOffset, _lastDeadZonePosition.y - _downBorderOffset, _targetToFollow.position.y + _cameraPositionOffset)
                , currentPosition.z);

            transform.position = Vector3.Lerp(currentPosition, targetPosition, _followSpeed * Time.deltaTime);    
        }
    }
}
