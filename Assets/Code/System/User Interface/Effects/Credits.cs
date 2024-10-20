using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Toulouse.Effects
{
    public class Credits : MonoBehaviour
    {
        [SerializeField] private float _target;
        [SerializeField, Range(0, 10)] private float _speed, _max;
        [SerializeField] private UnityEvent _onComplete;

        private RectTransform rect;
        private float _width, _limit, _current;
        private bool _isDone;

        private void Awake() => rect = transform as RectTransform;
        private void Start() { _current = _speed; _width = rect.rect.width; _limit = _target + _width * 0.01f; }
        private void Update() => OnComplete();
        private void FixedUpdate() => rect.position = _current * Time.fixedDeltaTime * math.left() + (float3)rect.position;

        public void OnPress(BaseEventData data) => _current = _max;
        public void OnRelease(BaseEventData data) => _current = _speed;

        private void OnComplete()
        {
            if (_isDone) return;
            if (rect.position.x > _limit) return;
            _onComplete.Invoke(); _isDone = true; print("credits completed!");
        }
        private void OnDrawGizmosSelected()
        {
            Vector2 up = new(_target, 1);
            Vector2 down = new(_target, -1);
            Gizmos.color = Color.red; Gizmos.DrawLine(up, down);
        }
    }
}