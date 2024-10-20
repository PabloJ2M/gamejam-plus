using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace UI.Inputs
{
    public class OnScreenTouchPosition : OnScreenTouch
    {
        [SerializeField] protected UnityEvent<Vector3> _onPosition;

        private Camera _camera;

        protected override void Awake() { base.Awake(); _camera = Camera.main; }
        protected override void Start() { base.Start(); _actions.UI.Point.performed += OnPointPosition; }

        protected Vector2 WorldPosition(Vector2 input) => _camera.ScreenToWorldPoint(input);

        protected virtual void OnPointPosition(InputAction.CallbackContext ctx)
        {
            Vector2 input = ctx.ReadValue<Vector2>();
            _onPosition.Invoke(WorldPosition(input));
        }
    }
}