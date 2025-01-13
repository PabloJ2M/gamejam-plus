using UnityEngine;

namespace Events.Render
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorController : MonoBehaviour
    {
        private Animator _animator;

        private void Awake() => _animator = GetComponent<Animator>();

        public void Sequence() => _animator.SetTrigger("Complete");
    }
}