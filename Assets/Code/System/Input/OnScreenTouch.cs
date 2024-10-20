using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace UI.Inputs
{
    public class OnScreenTouch : MonoBehaviour
    {
        protected InputSystem_Actions _actions;
        private EventSystem _system;

        protected virtual void Awake() { _system = EventSystem.current; _actions = new(); }
        protected virtual void Start() => _actions.UI.Click.performed += OnPointPressed;
        protected virtual void OnEnable() => _actions.Enable();
        protected virtual void OnDisable() => _actions.Disable();

        protected virtual void OnPointPressed(InputAction.CallbackContext ctx) { }

        protected bool IsPointerOverUI(Vector2 touchPosition)
        {
            if (!_system) return false;
            PointerEventData data = new(_system);
            List<RaycastResult> result = new();
            data.position = touchPosition;

            _system.RaycastAll(data, result);
            return result.Count > 0;
        }
    }
}