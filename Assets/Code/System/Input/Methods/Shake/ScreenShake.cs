using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace UI.Inputs
{
    public class ScreenShake : OnScreenTouchPosition
    {
        [SerializeField] private AdvancedOrientation _orientation;
        [SerializeField, Range(0, 1)] private float _threshold;
        [SerializeField, Range(0, 10)] private int _amount;

        [SerializeField] private UnityEvent _onSuccess;

        private bool _isForward;
        private int _current;

        public AdvancedOrientation Orientation { set => _orientation = value; }

        protected override void Start() { base.Start(); _actions.UI.Delta.performed += OnDeltaMovement; }

        private void OnDeltaMovement(InputAction.CallbackContext ctx)
        {
            if (!IsPressing) return;
            Vector2 direction = _orientation.GetOrientation();
            Vector2 delta = ctx.ReadValue<Vector2>();
            if (delta.magnitude < 5) return;

            float value = Vector2.Dot(direction, delta.normalized);
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