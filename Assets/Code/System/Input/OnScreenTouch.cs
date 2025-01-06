using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace UI.Inputs
{
    public class OnScreenTouch : MonoBehaviour
    {
        [SerializeField] private bool _ignoreUI;
        protected Actions _actions;
        private EventSystem _system;

        public bool IsPressing { get; private set; }

        protected virtual void Awake() { _system = EventSystem.current; _actions = new(); }
        protected virtual void Start() => _actions.UI.Click.performed += OnPointPressed;
        protected virtual void OnDestroy() => _actions.UI.Click.performed -= OnPointPressed;
        protected virtual void OnEnable() => _actions.Enable();
        protected virtual void OnDisable() => _actions.Disable();

        protected virtual void OnPointPressed(InputAction.CallbackContext ctx) => IsPressing = ctx.action.IsPressed();

        protected bool IsPointerOverUI(Vector2 touchPosition)
        {
            if (!_system || _ignoreUI) return false;
            PointerEventData data = new(_system);
            List<RaycastResult> result = new();
            data.position = touchPosition;

            _system.RaycastAll(data, result);
            return result.Count > 0;
        }
    }
}