using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace UI.Inputs
{
    public class OnScreenTouchPosition : OnScreenTouch
    {
        [SerializeField] private bool _singleTouch;
        [SerializeField] protected UnityEvent<Vector3> _onPosition;

        private Camera _camera;

        protected override void Awake() { base.Awake(); _camera = Camera.main; }
        protected override void Start() { base.Start(); _actions.UI.Point.performed += OnPointPosition; }

        protected Vector2 WorldPosition(Vector2 input) => _camera.ScreenToWorldPoint(input);

        protected override void OnPointPressed(InputAction.CallbackContext ctx)
        {
            base.OnPointPressed(ctx);
            if (!_singleTouch || !IsPressing) return;

            Vector2 input = _actions.UI.Point.ReadValue<Vector2>();
            if (IsPointerOverUI(input)) return;
            
            _onPosition.Invoke(WorldPosition(input));
        }
        protected virtual void OnPointPosition(InputAction.CallbackContext ctx)
        {
            if (_singleTouch || !_actions.UI.Click.IsPressed()) return;

            Vector2 input = ctx.ReadValue<Vector2>();
            if (IsPointerOverUI(input)) return;

            _onPosition.Invoke(WorldPosition(input));
        }
    }
}