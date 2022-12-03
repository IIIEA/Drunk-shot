using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LineRenderer))]
public class Projection : MonoBehaviour
{
    private const float DefaultDotScale = 1f;

    [Header("Visual trajectory settings")]
    [SerializeField] private GameObject _dotPrefab;
    [SerializeField] private int _maxVisualPoints;
    [Range(0, 100)]
    [SerializeField] private int _minDotScale;
    [Space]
    [SerializeField] private int _maxPhysicsFrameIterations = 10;
    [SerializeField] private Transform _obstaclesParent;

    private Vector3 _dotScale;
    private Scene _simulationScene;
    private PhysicsScene _physicsScene;
    private List<Transform> _visualPoints = new List<Transform>();
    private readonly Dictionary<Transform, Transform> _spawnedObjects = new Dictionary<Transform, Transform>();

    private float _endScale => _minDotScale / 100f;

    private void Start()
    {
        CreatePoints();

        CreatePhysicsScene();
    }

    private void CreatePoints()
    {
        for (int i = 0; i < _maxVisualPoints; i++)
        {
            Transform tr = Instantiate(_dotPrefab).transform;
            tr.SetParent(transform);
            _visualPoints.Add(tr);
        }

        if (_visualPoints.Count != 0)
        {
            _dotScale = _visualPoints[0].localScale;
            _dotScale.z = 1f;
        }
    }

    private void CreatePhysicsScene()
    {
        _simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScene = _simulationScene.GetPhysicsScene();

        foreach (Transform obj in _obstaclesParent)
        {
            var ghostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
            ghostObj.GetComponent<Renderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(ghostObj, _simulationScene);
            if (!ghostObj.isStatic) _spawnedObjects.Add(obj, ghostObj.transform);
        }
    }

    private void Update()
    {
        foreach (var item in _spawnedObjects)
        {
            item.Value.position = item.Key.position;
            item.Value.rotation = item.Key.rotation;
        }
    }

    public void SimulateTrajectory(Rigidbody ballPrefab, Vector3 pos, Vector3 velocity)
    {
        var ghostObj = Instantiate(ballPrefab, pos, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(ghostObj.gameObject, _simulationScene);

        ghostObj.isKinematic = false;
        ghostObj.AddForce(velocity, ForceMode.Impulse);

        for (var i = 0; i < _visualPoints.Count; i++)
        {
            _physicsScene.Simulate(Time.fixedDeltaTime);
            _visualPoints[i].position = ghostObj.transform.position;
            _visualPoints[i].localScale = _dotScale * Remap.DoRemap(0, _visualPoints.Count, DefaultDotScale, _endScale, i);
        }

        Destroy(ghostObj.gameObject);
    }
}