using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace UnityEngine.InputSystem
{
    [Flags] public enum UIInteraction { Nothing = 0, SelfOnly = 1, AllChildren = 2 }

    public abstract class ActionsBehaviour : MonoBehaviour
    {
        protected Actions _inputs;

        protected virtual void Awake() => _inputs = new Actions();
        protected virtual void OnEnable() => _inputs.Enable();
        protected virtual void OnDisable() => _inputs.Disable();
    }
    public abstract class InteractionBehaviour : ActionsBehaviour
    {
        [SerializeField] protected UIInteraction _ignoreObjects;

        protected EventSystem _system;

        protected override void Awake() { base.Awake(); _system = EventSystem.current; }

        private List<RaycastResult> BaseResults()
        {
            if (!_system) return new();

            PointerEventData data = new(_system) { position = _inputs.UI.Point.ReadValue<Vector2>() };
            List<RaycastResult> result = new();
            _system.RaycastAll(data, result);

            result.RemoveAll(x => x.gameObject.layer == LayerMask.NameToLayer("TransparentFX"));
            return result;
        }
        private List<RaycastResult> ClearResults()
        {
            if (_ignoreObjects.Equals(UIInteraction.Nothing)) return new();
            var result = BaseResults().Where(r => r.gameObject.layer != LayerMask.NameToLayer("Ignore Raycast")).ToList();
            
            if (_ignoreObjects.HasFlag(UIInteraction.SelfOnly)) result.RemoveAll(x => x.gameObject.Equals(gameObject));
            if (_ignoreObjects.HasFlag(UIInteraction.AllChildren)) result.RemoveAll(x => x.gameObject.transform.IsChildOf(transform));
            return result;
        }

        public bool IsPointerOverObject(GameObject element)
        {
            var results = BaseResults();
            if (results.Count == 0) return false;

            var list = results.OrderByDescending(r => r.sortingLayer).ThenByDescending(r => r.sortingOrder).ThenByDescending(r => r.depth);
            print(list.First().gameObject.name);
            return list.First().gameObject == element;
        }
        protected bool IsPointerOverUI() => ClearResults().Count > 0;
    }
}