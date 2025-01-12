using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshAgentCore : MonoBehaviour
{
    private NavMeshAgent _agent;

    public NavMeshAgent Agent => _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = _agent.updateUpAxis = false;
    }

    public void Position(Vector2 position) => _agent.Warp(position);
}
