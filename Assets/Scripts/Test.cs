using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private Rigidbody _ball;

    public float Power = 100;

    public Projection Trajectory;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        float enter;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        new Plane(-Vector3.forward, transform.position).Raycast(ray, out enter);
        Vector3 mouseInWorld = ray.GetPoint(enter);

        Vector3 speed = (mouseInWorld - transform.position) * Power;
        transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, speed.y));
        Trajectory.SimulateTrajectory(_ball, transform.position, speed);
    }
}