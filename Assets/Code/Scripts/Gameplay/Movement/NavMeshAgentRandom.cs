using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgentCore))]
public class NavMeshAgentRandom : MonoBehaviour
{
    [SerializeField] private CinemachineConfiner2D _confiner;

    private NavMeshAgentCore _core;
    private WaitUntil _waitUntil;
    private WaitForSeconds _delay = new(3);

    private void Awake() => _core = GetComponent<NavMeshAgentCore>();
    private void OnEnable() => _waitUntil = new(() => _core.Agent.remainingDistance < _core.Agent.radius);

    private IEnumerator Start()
    {
        _core.Agent.SetDestination(RandomPoint());
        
        yield return _waitUntil;
        yield return _delay;

        StartCoroutine(Start());
    }
    private Vector2 RandomPoint()
    {
        Bounds bound = _confiner.BoundingShape2D.bounds;
        return new(Random.Range(bound.min.x, bound.max.x), Random.Range(bound.min.y, bound.max.y));
    }
}