using UnityEngine;

[RequireComponent(typeof(NavMeshAgentCore))]
public class NavMeshAgentAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private NavMeshAgentCore _core;
    private SpriteRenderer _render;

    private void Awake() => _core = GetComponent<NavMeshAgentCore>();
    private void Start() => _render = _animator.GetComponent<SpriteRenderer>();
    private void Update()
    {
        _animator.SetFloat("Velocity", _core.Agent.velocity.magnitude);

        if (_core.Agent.velocity == Vector3.zero) return;

        _render.flipX = _core.Agent.velocity.x > 0;
        _animator.SetFloat("X", _core.Agent.velocity.x);
        _animator.SetFloat("Y", _core.Agent.velocity.y);
    }
}