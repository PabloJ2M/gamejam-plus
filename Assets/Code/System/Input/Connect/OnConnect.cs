using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace UI.Inputs.Drag
{
    [RequireComponent(typeof(LineRenderer))]
    public class OnConnect : OnScreenTouchPosition
    {
        [SerializeField] private UnityEvent _onComplete;

        private bool _isPressed, _isCompleted;
        private LineRenderer _renderer;
        private Vector2 _target;

        protected override void Awake() { base.Awake(); _renderer = GetComponent<LineRenderer>(); }
        protected override void OnEnable() { base.OnEnable(); _renderer.positionCount = 1; _onPosition.AddListener(TrailPosition); }
        protected override void OnDisable() { base.OnDisable(); _onPosition.RemoveAllListeners(); }
        private void TrailPosition(Vector2 point) => _target = point;

        private void Update()
        {
            if (!_isPressed || _renderer.positionCount > transform.childCount) return;
            _renderer.SetPosition(_renderer.positionCount - 1, _target);
        }
        protected override void OnPointPressed(InputAction.CallbackContext ctx)
        {
            _isPressed = ctx.action.IsPressed();
            if (_isPressed || _isCompleted) return;
            SetDefault();
        }

        public void SetDefault() { _isCompleted = false; _renderer.positionCount = 1; /*_renderer.SetPosition(0, Vector2.zero);*/ }
        public bool IsCurrent(int index) => _isPressed && _renderer.positionCount - 1 == index;
        public void AddPoint(Point point)
        {
            _renderer.positionCount = Mathf.Clamp(_renderer.positionCount + 1, 0, transform.childCount + 1);
            _renderer.SetPosition(point.Index, point.Position);

            if (_renderer.positionCount <= transform.childCount) return;
            _renderer.SetPosition(_renderer.positionCount - 1, point.Position);
            _onComplete.Invoke();
            _isCompleted = true;
        }
    }
}