using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace UI.Inputs
{
    public class OnScreenTouchPosition : MonoBehaviour
    {
        [SerializeField] protected UnityEvent<Vector2> _onPosition;

        private InputSystem_Actions _actions;
        private Camera _camera;

        protected virtual void Awake() { _actions = new(); _camera = Camera.main; }
        protected virtual void Start() { _actions.UI.Point.performed += OnPointPosition; _actions.UI.Click.performed += OnPointPressed; }
        protected virtual void OnEnable() => _actions.Enable();
        protected virtual void OnDisable() => _actions.Disable();

        protected virtual void OnPointPressed(InputAction.CallbackContext ctx) { }
        protected virtual void OnPointPosition(InputAction.CallbackContext ctx)
        {
            if (!_actions.UI.Click.IsPressed()) return;
            Vector2 position = _camera.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
            _onPosition.Invoke(position);
        }
    }
}