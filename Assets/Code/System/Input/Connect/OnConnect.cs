using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace UI.Inputs.Drag
{
    [RequireComponent(typeof(LineRenderer))]
    public class OnConnect : OnScreenTouchPosition
    {
        [SerializeField] private UnityEvent _onComplete;

        private LineRenderer _renderer;
        private bool _isCompleted;
        private Vector2 _target;

        protected override void Awake() { base.Awake(); _renderer = GetComponent<LineRenderer>(); }
        protected override void OnEnable() { base.OnEnable(); _renderer.positionCount = 1; _onPosition.AddListener(TrailPosition); }
        protected override void OnDisable() { base.OnDisable(); _onPosition.RemoveAllListeners(); }
        private void TrailPosition(Vector3 point) => _target = point;

        private void Update()
        {
            if (!IsPressing || _renderer.positionCount > transform.childCount) return;
            _renderer.SetPosition(_renderer.positionCount - 1, _target);
        }
        protected override void OnPointPressed(InputAction.CallbackContext ctx)
        {
            base.OnPointPressed(ctx);
            if (IsPressing || _isCompleted) return;
            SetDefault();
        }

        public void SetDefault() { _isCompleted = false; _renderer.positionCount = 1; }
        public void AddPoint(Point point)
        {
            //block duplicated positions
            for (int i = 0; i < _renderer.positionCount; i++) { if ((Vector2)_renderer.GetPosition(i) == point.Position) return; }

            _renderer.positionCount = Mathf.Clamp(_renderer.positionCount + 1, 0, transform.childCount + 1);
            _renderer.SetPosition(_renderer.positionCount - 2, point.Position);

            if (_renderer.positionCount <= transform.childCount) return;
            _renderer.SetPosition(_renderer.positionCount - 1, point.Position);
            _onComplete.Invoke();
            _isCompleted = true;
        }
    }
}