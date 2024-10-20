using UnityEngine;
using UnityEngine.Events;

namespace UI.Inputs
{
    public class OnShake : OnScreenTouchPosition
    {
        [SerializeField] private Orientation _orientation;
        [SerializeField, Range(0, 1)] private float _threshold;
        [SerializeField, Range(0, 10)] private int _amount;

        [SerializeField] private UnityEvent _onSuccess;

        private bool _isForward;
        private int _current;

        public Orientation Orientation { set => _orientation = value; }

        protected override void OnEnable() { base.OnEnable(); _onPosition.AddListener(PositionPerfome); }
        protected override void OnDisable() { base.OnDisable(); _onPosition.RemoveAllListeners(); }

        private void PositionPerfome(Vector3 point)
        {
            if (!IsPressing) return;
            Vector2 direction = _orientation.GetOrientation();
            Vector2 movement = (point - transform.position).normalized;

            float value = Vector2.Dot(direction, movement);
            if (value > _threshold && _isForward) { MatchDirection(true); }
            if (value < -_threshold && !_isForward) { MatchDirection(false); }

            void MatchDirection(bool isForward)
            {
                _isForward = !isForward;
                _current++;

                if (_current < _amount) return;
                _onSuccess.Invoke();
                _current = 0;
            }
        }
    }
}