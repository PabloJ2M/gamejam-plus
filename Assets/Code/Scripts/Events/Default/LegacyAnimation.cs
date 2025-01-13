using UnityEngine;

namespace Events.Gameplay
{
    [RequireComponent(typeof(Animation))]
    public class LegacyAnimation : MonoBehaviour
    {
        private Animation _animation;

        private void Awake() => _animation = GetComponent<Animation>();
        [ContextMenu("Play")] public void StartAnimation() => _animation.Play();
    }
}