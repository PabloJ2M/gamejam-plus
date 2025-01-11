using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    private SpriteRenderer _render;
    private NavMeshAgent _agent;
    private Animator _animator;

    private void Start() => _agent.updateRotation = _agent.updateUpAxis = false;
    
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _render = GetComponentInChildren<SpriteRenderer>();
        _agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        _animator.SetFloat("Velocity", _agent.velocity.magnitude);

        if (_agent.velocity == Vector3.zero) return;

        _render.flipX = _agent.velocity.x > 0;
        _animator.SetFloat("X", _agent.velocity.x);
        _animator.SetFloat("Y", _agent.velocity.y);
    }
}