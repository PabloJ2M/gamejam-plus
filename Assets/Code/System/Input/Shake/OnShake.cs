using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace UI.Inputs
{
    public class OnShake : OnScreenTouchPosition
    {
        [SerializeField] private Orientation _orientation;
        [SerializeField, Range(0, 1)] private float _threshold;
        [SerializeField] private UnityEvent _onForward, _onBackward;

        private Vector3 _lastPosition;
        private bool _isForward;

        public Orientation Orientation { set => _orientation = value; }

        protected override void OnEnable() { base.OnEnable(); _onPosition.AddListener(PositionPerfome); }
        protected override void OnDisable() { base.OnDisable(); _onPosition.RemoveAllListeners(); }

        protected override void OnPointPressed(InputAction.CallbackContext ctx)
        {
            Vector2 input = _actions.UI.Point.ReadValue<Vector2>();
            if (IsPointerOverUI(input)) return;

            Vector2 position = WorldPosition(input);
            _lastPosition = ctx.action.IsPressed() ? position : Vector3.zero;
        }
        private void PositionPerfome(Vector3 point)
        {
            if (_lastPosition == Vector3.zero) return;
            Vector2 direction = _orientation.GetOrientation();
            Vector2 movement = _lastPosition - point;

            float value = Vector2.Dot(direction, movement);
            if (value > _threshold && _isForward) { MatchDirection(true); }
            if (value < -_threshold && !_isForward) { MatchDirection(false); }

            void MatchDirection(bool isForward)
            {
                if (isForward) _onForward.Invoke(); else _onBackward.Invoke();
                _isForward = !isForward;
                _lastPosition = point;
            }
        }
    }
}