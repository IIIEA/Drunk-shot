using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LineRenderer))]
public class Projection : MonoBehaviour
{
    private const float DefaultDotScale = 1f;

    [Header("Visual trajectory settings")]
    [SerializeField] private DotVisual _visualPointPrefab;
    [SerializeField] private int _maxVisualPoints;
    [Range(0, 100)]
    [SerializeField] private int _minDotScale;
    [Space]
    [SerializeField] private Transform _obstaclesParent;

    private Vector3 _dotScale;
    private Scene _simulationScene;
    private PhysicsScene2D _physicsScene;
    private List<DotVisual> _visualPoints = new List<DotVisual>();
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
            DotVisual visualPoint = Instantiate(_visualPointPrefab);
            visualPoint.transform.SetParent(transform);
            _visualPoints.Add(visualPoint);
        }

        if (_visualPoints.Count != 0)
        {
            _dotScale = _visualPoints[0].transform.localScale;
            _dotScale.z = 1f;
        }
    }

    private void CreatePhysicsScene()
    {
        _simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics2D));
        _physicsScene = _simulationScene.GetPhysicsScene2D();

        foreach (Transform obj in _obstaclesParent)
        {
            var ghostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
            ghostObj.GetComponent<Renderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(ghostObj, _simulationScene);

            if (!ghostObj.isStatic)
                _spawnedObjects.Add(obj, ghostObj.transform);
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

    public void SimulateTrajectory(Rigidbody2D ballPrefab, Vector3 pos, Vector3 velocity)
    {
        var ghostObj = Instantiate(ballPrefab, pos, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(ghostObj.gameObject, _simulationScene);

        ghostObj.simulated = true;
        velocity.z = 0;
        ghostObj.AddForce(velocity, ForceMode2D.Impulse);

        for (var i = 0; i < _visualPoints.Count; i++)
        {
            _physicsScene.Simulate(Time.fixedDeltaTime);
            _visualPoints[i].transform.position = ghostObj.transform.position;
            _visualPoints[i].transform.localScale = _dotScale * Remap.DoRemap(0, _visualPoints.Count, DefaultDotScale, _endScale, i);
        }

        Destroy(ghostObj.gameObject);
    }

    public void SetDotsAlpha(float alpha)
    {
        for (int i = 0; i < _visualPoints.Count; i++)
        {
            _visualPoints[i].SetAlpha(alpha);
        }
    }
}