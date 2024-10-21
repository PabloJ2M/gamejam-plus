using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI.Events
{
    public interface IInteractable
    {
        public Vector2 WorldCoords { get; }
    }

    public class KeyInteraction : SingletonBasic<KeyInteraction>
    {
        [SerializeField] private RectTransform _icon;
        [SerializeField] private UnityEvent _onDisplay, _onHide;

        private IInteractable _area;
        public Action onInteract;

        public void SetInteractable(IInteractable area) { _area = area; _onDisplay.Invoke(); }
        public void RemoveInteractable() { _area = null; _onHide.Invoke(); }
        public void Interact(BaseEventData data) => onInteract?.Invoke();

        private void Update()
        {
            if (_area == null) return;

            _icon.position = 1f * Vector2.up + _area.WorldCoords;
        }
    }
}