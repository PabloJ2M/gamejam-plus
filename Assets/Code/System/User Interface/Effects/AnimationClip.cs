using UnityEngine;

namespace UI.Effects
{
    [RequireComponent(typeof(Animator))]
    public class AnimationClip : MonoBehaviour
    {
        [SerializeField] private string _default;
        [SerializeField] private float _speed;
        private Animator _animator;

        private void Awake() => _animator = GetComponent<Animator>();
        private void Start() => _animator.Play(_default);
        private void Update() => _animator.SetFloat("Speed", _speed);
    }
}