using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace UnityEngine.InputSystem
{
    public abstract class ActionsBehaviour : MonoBehaviour
    {
        protected Actions _inputs;

        protected virtual void Awake() => _inputs = new Actions();
        protected virtual void OnEnable() => _inputs.Enable();
        protected virtual void OnDisable() => _inputs.Disable();
    }
    public abstract class InteractionBehaviour : ActionsBehaviour
    {
        [Flags] private enum UIInteraction { Nothing = 0, SelfOnly = 1, AllChildren = 2 }
        [SerializeField] private UIInteraction _ignoreObjects;

        protected EventSystem _system;

        protected override void Awake() { base.Awake(); _system = EventSystem.current; }

        protected bool IsPointerOverUI()
        {
            if (!_system || _ignoreObjects.Equals(UIInteraction.Nothing)) return false;

            PointerEventData data = new(_system) { position = _inputs.UI.Point.ReadValue<Vector2>() };
            List<RaycastResult> result = new();

            _system.RaycastAll(data, result);

            result.RemoveAll(x => x.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast"));
            if (_ignoreObjects.HasFlag(UIInteraction.SelfOnly)) result.RemoveAll(x => x.gameObject.Equals(gameObject));
            if (_ignoreObjects.HasFlag(UIInteraction.AllChildren)) result.RemoveAll(x => x.gameObject.transform.IsChildOf(transform));
            foreach (var item in result) print(item.gameObject.name);
            return result.Count > 0;
        }
    }
}