using System;
using System.Linq;
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

        private List<RaycastResult> Results()
        {
            if (!_system || _ignoreObjects.Equals(UIInteraction.Nothing)) return new();

            PointerEventData data = new(_system) { position = _inputs.UI.Point.ReadValue<Vector2>() };
            List<RaycastResult> result = new();
            _system.RaycastAll(data, result);

            result.RemoveAll(x => x.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast"));
            if (_ignoreObjects.HasFlag(UIInteraction.SelfOnly)) result.RemoveAll(x => x.gameObject.Equals(gameObject));
            if (_ignoreObjects.HasFlag(UIInteraction.AllChildren)) result.RemoveAll(x => x.gameObject.transform.IsChildOf(transform));
            return result;
        }

        protected bool IsPointerOverUI() => Results().Count > 0;
        protected bool IsPointerOverUI(params GameObject[] args)
        {
            var list = Results();
            list.RemoveAll(x => args.Contains(x.gameObject));
            return list.Count > 0;
        }
    }
}